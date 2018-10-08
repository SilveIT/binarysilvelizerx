using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BinarySilvelizerX.Common;
using BinarySilvelizerX.SerializerNodes;

// ReSharper disable UnusedMember.Global

namespace BinarySilvelizerX.Core
{
    public static class SerializerTypeCache
    {
        internal static Dictionary<Type, Task<List<BasicNode>>> CacheTasks { get; } = new Dictionary<Type, Task<List<BasicNode>>>();
        internal static Dictionary<Type, List<BasicNode>> CacheStorage { get; } = new Dictionary<Type, List<BasicNode>>();
        public static bool Enabled { get; set; } = true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static async Task<List<BasicNode>> GetNodes(Type type)
        {
            if (CacheStorage.ContainsKey(type))
                return CacheStorage[type];
            if (CacheTasks.ContainsKey(type))
                return await CacheTasks[type];
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Cache(Type type, List<BasicNode> nodes)
        {
            if (!CacheStorage.ContainsKey(type))
                CacheStorage.Add(type, nodes);
            else
                Logger.Write($"Type {type} is already cached! Please report to developer if you are getting this error!", "AutoCache", Logger.MessageType.Error);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cache(Type type)
        {
            if (CacheStorage.ContainsKey(type))
            {
                Logger.Write($"Type {type} is already cached!", "Cache", Logger.MessageType.Warning);
                return;
            }
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