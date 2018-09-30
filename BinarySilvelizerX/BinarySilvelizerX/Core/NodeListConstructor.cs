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
            var cacheEnabled = SerializerTypeCacheController.Enabled;
            if (cacheEnabled && SerializerTypeCacheController.CacheStorage.ContainsKey(sourceType))
                nodes = SerializerTypeCacheController.CacheStorage[sourceType];
            else
            {
                nodes = GenerateNodes(sourceType);
                if (cacheEnabled)
                    SerializerTypeCacheController.CacheStorage.Add(sourceType, nodes);
            }
            return nodes;
        }

        private static List<BasicNode> GenerateNodes(Type sourceType)
        {
            var nodes = new List<BasicNode>();
            var props = PropListConstructor.GeneratePropertyInfoList(sourceType);
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
                        throw new Exception($"Type {type} is not supported!");
                }
            }
            return nodes;
        }
    }
}