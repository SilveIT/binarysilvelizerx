using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BinarySilvelizerX.Streams
{
    public class ExtendedReader : BinaryReader
    {
        public ExtendedReader(Stream input) : base(input)
        {
        }

        public ExtendedReader(Stream input, Encoding encoding) : base(input, encoding)
        {
        }

        public ExtendedReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
        {
        }

        public async Task<ushort> ReadUInt16Async(CancellationToken cancellationToken)
        {
            var data = new byte[2];
            await BaseStream.ReadAsync(data, 0, data.Length, cancellationToken)
                .ConfigureAwait(false);
            return BitConverter.ToUInt16(data, 0);
        }

        public async Task<short> ReadInt16Async(CancellationToken cancellationToken)
        {
            var data = new byte[2];
            await BaseStream.ReadAsync(data, 0, data.Length, cancellationToken)
                .ConfigureAwait(false);
            return BitConverter.ToInt16(data, 0);
        }

        public async Task<uint> ReadUInt32Async(CancellationToken cancellationToken)
        {
            var data = new byte[4];
            await BaseStream.ReadAsync(data, 0, data.Length, cancellationToken)
                .ConfigureAwait(false);
            return BitConverter.ToUInt32(data, 0);
        }

        public async Task<int> ReadInt32Async(CancellationToken cancellationToken)
        {
            var data = new byte[4];
            await BaseStream.ReadAsync(data, 0, data.Length, cancellationToken)
                .ConfigureAwait(false);
            return BitConverter.ToInt32(data, 0);
        }

        public async Task<ulong> ReadUInt64Async(CancellationToken cancellationToken)
        {
            var data = new byte[8];
            await BaseStream.ReadAsync(data, 0, data.Length, cancellationToken)
                .ConfigureAwait(false);
            return BitConverter.ToUInt64(data, 0);
        }

        public async Task<long> ReadInt64Async(CancellationToken cancellationToken)
        {
            var data = new byte[8];
            await BaseStream.ReadAsync(data, 0, data.Length, cancellationToken)
                .ConfigureAwait(false);
            return BitConverter.ToInt64(data, 0);
        }

        public async Task<float> ReadSingleAsync(CancellationToken cancellationToken)
        {
            var data = new byte[4];
            await BaseStream.ReadAsync(data, 0, data.Length, cancellationToken)
                .ConfigureAwait(false);
            return BitConverter.ToSingle(data, 0);
        }

        public async Task<double> ReadDoubleAsync(CancellationToken cancellationToken)
        {
            var data = new byte[8];
            await BaseStream.ReadAsync(data, 0, data.Length, cancellationToken)
                .ConfigureAwait(false);
            return BitConverter.ToDouble(data, 0);
        }

        public async Task<byte[]> ReadBytesAsync(int count, CancellationToken cancellationToken)
        {
            var data = new byte[count];
            await BaseStream.ReadAsync(data, 0, data.Length, cancellationToken)
                .ConfigureAwait(false);
            return data;
        }

        public async Task<byte> ReadByteAsync(CancellationToken cancellationToken)
        {
            var data = new byte[1];
            await BaseStream.ReadAsync(data, 0, data.Length, cancellationToken)
                .ConfigureAwait(false);
            return data[0];
        }

        public async Task<sbyte> ReadSByteAsync(CancellationToken cancellationToken)
        {
            var data = new byte[1];
            await BaseStream.ReadAsync(data, 0, data.Length, cancellationToken)
                .ConfigureAwait(false);
            return (sbyte)data[0];
        }
    }
}