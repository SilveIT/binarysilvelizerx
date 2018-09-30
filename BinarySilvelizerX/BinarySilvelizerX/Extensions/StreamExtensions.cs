using System.IO;

namespace BinarySilvelizerX.Extensions
{
    public static class StreamExtensions
    {
        public static long AvailableLength(this BinaryReader reader)
        {
            var stream = (MemoryStream)reader.BaseStream;
            return stream.Length - stream.Position;
        }
    }
}