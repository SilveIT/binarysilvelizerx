using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BinarySilvelizerX.Common;
using BinarySilvelizerX.SerializerNodes;

namespace BinarySilvelizerX.Core
{
    public static class SerializerTypeCacheController
    {
        internal static Dictionary<Type, List<BasicNode>> CacheStorage { get; } = new Dictionary<Type, List<BasicNode>>();
        public static bool Enabled { get; set; } = true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cache(Type type)
        {
            CacheStorage.Add(type, NodeListController.GetNodes(type));
            Logger.Write($"Cached type {type} by manual request", "Cache", Logger.MessageType.Info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Remove(Type type)
        {
            CacheStorage.Remove(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearCache()
        {
            CacheStorage.Clear();
        }
    }
}