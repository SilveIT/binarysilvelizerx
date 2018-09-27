using System.IO;

namespace BinarySilvelizerX.Extensions
{
    public static class StreamExtensions
    {
        public static long AvailableLength(this Stream stream) => stream.Length - stream.Position;
    }
}