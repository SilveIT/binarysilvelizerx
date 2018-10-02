using System;
using System.Collections.Generic;
using BinarySilvelizerX.Common;
using BinarySilvelizerX.Extensions;
using BinarySilvelizerX.SerializerNodes;

namespace BinarySilvelizerX.Core
{
    internal static class NodeListController
    {
        internal static List<BasicNode> GetNodes(Type sourceType)
        {
            List<BasicNode> nodes;
            var cacheEnabled = SerializerTypeCache.Enabled;
            if (cacheEnabled && SerializerTypeCache.CacheStorage.ContainsKey(sourceType))
                nodes = SerializerTypeCache.CacheStorage[sourceType];
            else
            {
                nodes = GenerateNodes(sourceType); //TODO: think about caching nulls
                if (cacheEnabled)
                    SerializerTypeCache.CacheStorage.Add(sourceType, nodes);
            }
            return nodes;
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
                        nodes.Add(new ObjectNode(prop));
                }
            }
            return nodes;
        }
    }
}