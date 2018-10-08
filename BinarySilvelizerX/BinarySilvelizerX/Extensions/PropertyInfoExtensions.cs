using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

// ReSharper disable UnusedMember.Global

namespace BinarySilvelizerX.Extensions
{
    public static class PropertyInfoExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T GetFirstAttribute<T>(this ICustomAttributeProvider info) => (T)info.GetCustomAttributes(typeof(T), false).FirstOrDefault();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T[] GetAttributesArray<T>(this ICustomAttributeProvider info) => info.GetCustomAttributes(typeof(T), false).Cast<T>().ToArray();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T GetFirstAttribute<T>(this Type type) => (T)type.GetCustomAttributes(typeof(T), false).FirstOrDefault();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T[] GetAttributesArray<T>(this Type type) => type.GetCustomAttributes(typeof(T), false).Cast<T>().ToArray();
    }
}