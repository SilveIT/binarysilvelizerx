using System;
using System.Runtime.CompilerServices;
using System.Text;
using BinarySilvelizerX.Streams;
using BinarySilvelizerX.Utils;

namespace BinarySilvelizerX.PrimitiveSerializers
{
    internal static class StringSerializer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void WriteDynamic(ExtendedWriter writer, string input)
        {
            var encoding = Encoding.GetEncoding((int)TextUtils.CodePage.Windows1251);
            var strArr = encoding.GetBytes(input);
            var bytesPerChar = encoding.GetByteCount("0");
            writer.Write(input.Length + 1);
            writer.Write(strArr);
            writer.Write(BufferHelper.GetBytes(bytesPerChar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static string ReadDynamic(ExtendedReader reader)
        {
            var encoding = Encoding.GetEncoding((int)TextUtils.CodePage.Windows1251);
            var len = reader.ReadInt32();
            var bytesPerChar = encoding.GetByteCount("0");
            len *= bytesPerChar;
            return encoding.GetString(reader.ReadBytes(len)).Trim('\0');
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe byte[] WriteDynamic(string input)
        {
            var encoding = Encoding.GetEncoding((int)TextUtils.CodePage.Windows1251);
            var strArr = encoding.GetBytes(input);
            var ln = strArr.Length;
            var bytesPerChar = encoding.GetByteCount("0");
            var res = new byte[4 + ln + bytesPerChar]; //[int Ln][string Str][TerminalNull]
            fixed (byte* t = &res[0])
                *(int*)t = input.Length + 1;
            Buffer.BlockCopy(strArr, 0, res, 4, ln);
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe string ReadDynamic(byte[] array)
        {
            var encoding = Encoding.GetEncoding((int)TextUtils.CodePage.Windows1251);
            int len;
            fixed (byte* t = &array[0])
                len = *(int*)t;
            var bytesPerChar = encoding.GetByteCount("0");
            len *= bytesPerChar;
            return encoding.GetString(array, 4, len).Trim('\0');
        }
    }
}