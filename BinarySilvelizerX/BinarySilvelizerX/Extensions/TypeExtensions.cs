using System;
using System.Collections;
using System.Net;
using System.Runtime.CompilerServices;
using BinarySilvelizerX.Entities;

namespace BinarySilvelizerX.Extensions
{
    public static class TypeExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonPrimitiveStruct(this Type type) => type.IsValueType && !type.IsPrimitive &&
                                                                   !type.IsEnum;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsString(this Type type) => type == typeof(string);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSpacer(this Type type) => type == typeof(ByteSpacer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCollection(this Type type) => typeof(ICollection).IsAssignableFrom(type);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsClearValueType(this Type type) => !type.IsClass && !type.IsNonPrimitiveStruct() || type == typeof(decimal);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsIPAddress(this Type type) => type == typeof(IPAddress);
    }
}