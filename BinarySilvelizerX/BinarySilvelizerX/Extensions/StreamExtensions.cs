using System.IO;
using System.Runtime.CompilerServices;

namespace BinarySilvelizerX.Extensions
{
    public static class StreamExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long AvailableLength(this BinaryReader reader)
        {
            var stream = (MemoryStream)reader.BaseStream;
            return stream.Length - stream.Position;
        }
    }
}