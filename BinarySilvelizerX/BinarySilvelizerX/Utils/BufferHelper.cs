using System.Collections.Generic;

namespace BinarySilvelizerX.Utils
{
    internal static class BufferHelper
    {
        private static readonly Dictionary<int, byte[]> Bytes = new Dictionary<int, byte[]>();

        public static byte[] GetBytes(int count)
        {
            if (!Bytes.ContainsKey(count))
            {
                Bytes.Add(count, new byte[count]);
            }

            return Bytes[count];
        }
    }
}