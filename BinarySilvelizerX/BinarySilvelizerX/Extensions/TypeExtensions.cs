using System;
using System.Collections;
using System.Net;
using BinarySilvelizerX.Entities;

namespace BinarySilvelizerX.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsNonPrimitiveStruct(this Type type) => type.IsValueType && !type.IsPrimitive &&
                                                                   !type.IsEnum;

        public static bool IsString(this Type type) => type == typeof(string);

        public static bool IsSpacer(this Type type) => type == typeof(ByteSpacer);

        public static bool IsCollection(this Type type) => typeof(ICollection).IsAssignableFrom(type);

        public static bool IsClearValueType(this Type type) => !type.IsClass && !type.IsNonPrimitiveStruct() || type == typeof(decimal);

        public static bool IsIPAddress(this Type type) => type == typeof(IPAddress);
    }
}