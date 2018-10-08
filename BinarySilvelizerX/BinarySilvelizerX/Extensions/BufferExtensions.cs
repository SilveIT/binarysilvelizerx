using System.Text;

// ReSharper disable UnusedMember.Global

namespace BinarySilvelizerX.Extensions
{
    public static class BufferExtensions
    {
        public static string ToHex(this byte[] buffer)
        {
            var sb = new StringBuilder();
            sb.AppendLine("|---------------------------------------------------------------------------|");
            sb.AppendLine("|       00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F                     |");
            sb.AppendLine("|---------------------------------------------------------------------------|");
            var length = buffer.Length;
            var counter = 0;
            for (var i = 0; i < length; i++)
            {
                if (counter % 16 == 0)
                    sb.Append("| " + i.ToString("X4") + ": ");
                sb.Append(buffer[i].ToString("X2") + " ");
                counter++;
                if (counter != 16) continue;
                sb.Append("   ");
                var charPoint = i - 15;
                for (var j = 0; j < 16; j++)
                {
                    var n = buffer[charPoint++];
                    if (n > 0x1f && n < 0x80)
                        sb.Append((char)n);
                    else
                        sb.Append('.');
                }
                sb.Append(" |\r\n");
                counter = 0;
            }
            var rest = length % 16;
            if (rest > 0)
            {
                for (var i = 0; i < 17 - rest; i++)
                    sb.Append("   ");
                var charPoint = length - rest;
                for (var j = 0; j < rest; j++)
                {
                    var n = buffer[charPoint++];
                    if (n > 0x1f && n < 0x80)
                        sb.Append((char)n);
                    else
                        sb.Append('.');
                }
                sb.Append(' ', 16 - rest);
                sb.Append(" |\r\n");
            }
            sb.AppendLine("|---------------------------------------------------------------------------|");
            var text = sb.ToString();
            sb.Clear();
            return text;
        }
    }
}