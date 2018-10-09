using System;
using System.Collections.Concurrent;
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
        internal static ConcurrentDictionary<Type, Task<List<BasicNode>>> CacheTasks { get; }
            = new ConcurrentDictionary<Type, Task<List<BasicNode>>>();

        internal static ConcurrentDictionary<Type, List<BasicNode>> CacheStorage { get; }
            = new ConcurrentDictionary<Type, List<BasicNode>>();

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
        internal static void Cache(Type type, List<BasicNode> nodes) => CacheStorage.TryAdd(type, nodes);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cache(Type type)
        {
            if (!CacheStorage.TryAdd(type, NodeListController.GetNodes(type)))
            {
                Logger.Write($"Type {type} is already cached!", "Cache", Logger.MessageType.Warning);
                return;
            }
            Logger.Write($"Cached type {type} by manual request", "Cache", Logger.MessageType.Info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Remove(Type type) => CacheStorage.TryRemove(type, out _);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearCache() => CacheStorage.Clear();
    }
}