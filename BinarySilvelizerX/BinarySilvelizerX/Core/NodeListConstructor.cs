using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BinarySilvelizerX.Common;
using BinarySilvelizerX.Extensions;
using BinarySilvelizerX.SerializerNodes;
using BinarySilvelizerX.Utils;

namespace BinarySilvelizerX.Core
{
    internal static class NodeListController
    {
        internal static List<BasicNode> GetNodes(Type sourceType) //TODO: think about caching nulls
        {
            return SerializerTypeCache.Enabled
                ? AsyncHelper.RunSync(() => GetCachedNodesAsync(sourceType))
                : GenerateNodes(sourceType);
        }

        private static async Task<List<BasicNode>> GetCachedNodesAsync(Type sourceType)
        {
            var cachedNodes = await SerializerTypeCache.GetNodes(sourceType);
            if (cachedNodes != null) return cachedNodes;
            var task = Task.Run(() => GenerateNodes(sourceType)).ContinueWith(t =>
            {
                var result = t.Result;
                SerializerTypeCache.Cache(sourceType, result);
                SerializerTypeCache.CacheTasks.TryRemove(sourceType, out _);
                return result;
            });
            SerializerTypeCache.CacheTasks.TryAdd(sourceType, task);
            return await task;
        }

        private static List<BasicNode> GenerateNodes(Type sourceType)
        {
            var nodes = new List<BasicNode>();
            var props = PropListConstructor.GeneratePropertyInfoList(sourceType);
            if (props.Length == 0)
            {
                if (SerializerDefaults.ThrowIfNoSerializableNodesFound)
                    throw new Exception($"Type {sourceType} does not have any serializable properties!");
                return null;
            }
            foreach (var prop in props)
            {
                var type = prop.PropertyType;
                if (type.IsClearValueType())
                    nodes.Add(new ValueTypeNode(prop, type));
                else
                {
                    if (type.IsString())
                        nodes.Add(new StringNode(prop, new LengthInfo(prop, nodes)));
                    else if (type.IsCollection())
                    {
                        var lenInfo = new LengthInfo(prop, nodes);
                        if (type.IsArray)
                            nodes.Add(new ArrayNode(prop, lenInfo));
                        else
                            nodes.Add(new ListNode(prop, lenInfo));
                    }
                    else if (type.IsSpacer())
                        nodes.Add(new SpacerNode(prop));
                    else if (type.IsIPAddress())
                        nodes.Add(new IPAddressNode(prop));
                    else
                        nodes.Add(new ObjectNode(prop, new SubtypeInfo(prop, nodes)));
                }
            }
            return nodes;
        }
    }
}