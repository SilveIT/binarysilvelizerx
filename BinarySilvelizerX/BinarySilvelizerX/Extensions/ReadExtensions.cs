using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace BinarySilvelizerX.Extensions
{
    public static class ReadExtensions
    {
        private static readonly IReadOnlyDictionary<Type, Func<BinaryReader, object>> Casts =
            new Dictionary<Type, Func<BinaryReader, object>>
            {
                {typeof(bool), br => br.ReadBoolean()},
                {typeof(sbyte), br => br.ReadSByte()},
                {typeof(byte), br => br.ReadByte()},
                {typeof(char), br => br.ReadChar()},
                {typeof(short), br => br.ReadInt16()},
                {typeof(ushort), br => br.ReadUInt16()},
                {typeof(int), br => br.ReadInt32()},
                {typeof(uint), br => br.ReadUInt32()},
                {typeof(float), br => br.ReadSingle()},
                {typeof(long), br => br.ReadInt64()},
                {typeof(ulong), br => br.ReadUInt64()},
                {typeof(double), br => br.ReadDouble()},
                {typeof(string), br => br.ReadString().TrimEnd('\0')},
                {
                    typeof(PhysicalAddress), br =>
                    {
                        var bytes = br.ReadBytes(8);

                        return new PhysicalAddress(bytes);
                    }
                },
                {
                    typeof(IPAddress), br =>
                    {
                        var bytes = br.ReadBytes(4);

                        return new IPAddress(bytes);
                    }
                }
            };

        public static T Read<T>(this BinaryReader reader)
        {
            var type = typeof(T);
            var finalType = type.IsEnum ? type.GetEnumUnderlyingType() : type;

            return (T)Casts[finalType](reader);
        }
    }
}