using System;
using System.Collections;

namespace BinarySilvelizerX.Extensions
{
    public static class CollectionExtensions
    {
        public static Array ToArray(this IList coll, Type type, int length)
        {
            var arr = Array.CreateInstance(type, length);
            coll.CopyTo(arr, 0);
            return arr;
        }
    }
}