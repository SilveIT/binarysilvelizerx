using System;
using System.Linq;
using System.Reflection;

namespace BinarySilvelizerX.Extensions
{
    public static class PropertyInfoExtensions
    {
        internal static T GetFirstAttribute<T>(this PropertyInfo info) => (T)info.GetCustomAttributes(typeof(T), false).FirstOrDefault();

        internal static T[] GetAttributesArray<T>(this PropertyInfo info) => info.GetCustomAttributes(typeof(T), false).Cast<T>().ToArray();

        internal static T GetFirstAttribute<T>(this Type type) => (T)type.GetCustomAttributes(typeof(T), false).FirstOrDefault();

        internal static T[] GetAttributesArray<T>(this Type type) => type.GetCustomAttributes(typeof(T), false).Cast<T>().ToArray();
    }
}