using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using BinarySilvelizerX.Core;
using BinarySilvelizerX.Extensions;

namespace BinarySilvelizerX.PrimitiveSerializers
{
    internal class ValueTypeSerializer
    {
        internal static bool Write<T>(BinaryWriter writer, T value) => Write(writer, value, value.GetType());

        internal static T Read<T>(BinaryReader reader) => (T)Read(reader, typeof(T));

        internal static bool Write(BinaryWriter writer, object value, Type type)
        {
            var objType = value.GetType();
            if (type == null)
                type = objType;
            if (type.BaseType == typeof(Enum))
                type = Enum.GetUnderlyingType(type);
            var nobj = objType != type ? Convert.ChangeType(value, type) : value;
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Double: writer.Write((double)nobj); return true;
                case TypeCode.Single: writer.Write((float)nobj); return true;
                case TypeCode.Int16: writer.Write((short)nobj); return true;
                case TypeCode.Int64: writer.Write((long)nobj); return true;
                case TypeCode.UInt16: writer.Write((ushort)nobj); return true;
                case TypeCode.UInt32: writer.Write((uint)nobj); return true;
                case TypeCode.UInt64: writer.Write((ulong)nobj); return true;
                case TypeCode.Boolean: writer.Write((bool)nobj); return true;
                case TypeCode.Int32: writer.Write((int)nobj); return true;
                case TypeCode.Decimal:
                    WriteDecimal(writer, (decimal)nobj);
                    return true;

                case TypeCode.Byte:
                case TypeCode.SByte:
                    writer.Write((byte)nobj);
                    return true;

                default:
                    Debug.WriteLine($"[Error][BinarySilvelizer O2B] Type \"{type}\" is not supported, skipping!");
                    return false;
            }
        }

        internal static byte[] Write<T>(T value) => Write(value, value.GetType());

        internal static T Read<T>(byte[] data) => (T)Read(data, typeof(T));

        internal static byte[] Write(object value, Type type)
        {
            var objType = value.GetType();
            if (type == null)
                type = objType;
            if (type.BaseType == typeof(Enum))
                type = Enum.GetUnderlyingType(type);
            var nobj = objType != type ? Convert.ChangeType(value, type) : value;
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Double: return BitConverter.GetBytes((double)nobj);
                case TypeCode.Single: return BitConverter.GetBytes((float)nobj);
                case TypeCode.Int16: return BitConverter.GetBytes((short)nobj);
                case TypeCode.Int32: return BitConverter.GetBytes((int)nobj);
                case TypeCode.Int64: return BitConverter.GetBytes((long)nobj);
                case TypeCode.UInt16: return BitConverter.GetBytes((ushort)nobj);
                case TypeCode.UInt32: return BitConverter.GetBytes((uint)nobj);
                case TypeCode.UInt64: return BitConverter.GetBytes((ulong)nobj);
                case TypeCode.Boolean: return BitConverter.GetBytes((bool)nobj);
                case TypeCode.Decimal:
                    return WriteDecimal((decimal)nobj);

                case TypeCode.Byte:
                case TypeCode.SByte:
                    return new[] { (byte)nobj };

                default:
                    Debug.WriteLine($"[Error][BinarySilvelizer O2B] Type \"{type}\" is not supported, skipping!");
                    return null;
            }
        }

        internal static object Read(byte[] data, Type type)
        {
            if (type.BaseType == typeof(Enum))
                type = Enum.GetUnderlyingType(type);
            var available = data.Length;
            if (type == typeof(decimal) && available >= 16)
                return ReadDecimal(data);
            if (type == typeof(double) && available >= 8)
                return BitConverter.ToDouble(data, 0);
            if (type == typeof(short) && available >= 2)
                return BitConverter.ToInt16(data, 0);
            if (type == typeof(int) && available >= 4)
                return BitConverter.ToInt32(data, 0);
            if (type == typeof(long) && available >= 8)
                return BitConverter.ToInt64(data, 0);
            if (type == typeof(float) && available >= 4)
                return BitConverter.ToSingle(data, 0);
            if (type == typeof(ushort) && available >= 2)
                return BitConverter.ToUInt16(data, 0);
            if (type == typeof(uint) && available >= 4)
                return BitConverter.ToUInt32(data, 0);
            if (type == typeof(ulong) && available >= 8)
                return BitConverter.ToUInt64(data, 0);
            if (type == typeof(bool) && available >= 1)
                return BitConverter.ToBoolean(data, 0);
            if (type == typeof(byte) && available >= 1)
                return data[0];
            if (type == typeof(sbyte) && available >= 1)
                return (sbyte)data[0];
            Debug.WriteLine($"[Error][BinarySilvelizer] Type \"{type}\" is not supported, skipping!");
            return null;
        }

        internal static object Read(BinaryReader reader, Type type)
        {
            if (type.BaseType == typeof(Enum))
                type = Enum.GetUnderlyingType(type);
            var available = reader.BaseStream.AvailableLength();
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Double:
                    {
                        if (available >= 8) return reader.ReadDouble();
                        break;
                    }
                case TypeCode.Single:
                    {
                        if (available >= 4) return reader.ReadSingle();
                        break;
                    }
                case TypeCode.Int16:
                    {
                        if (available >= 2) return reader.ReadInt16();
                        break;
                    }
                case TypeCode.Int32:
                    {
                        if (available >= 4) return reader.ReadInt32();
                        break;
                    }
                case TypeCode.Int64:
                    {
                        if (available >= 8) return reader.ReadInt64();
                        break;
                    }
                case TypeCode.UInt16:
                    {
                        if (available >= 2) return reader.ReadUInt16();
                        break;
                    }
                case TypeCode.UInt32:
                    {
                        if (available >= 4) return reader.ReadUInt32();
                        break;
                    }
                case TypeCode.UInt64:
                    {
                        if (available >= 8) return reader.ReadUInt64();
                        break;
                    }
                case TypeCode.Boolean:
                    {
                        if (available >= 1) return reader.ReadBoolean();
                        break;
                    }
                case TypeCode.Decimal:
                    {
                        if (available >= 16) return ReadDecimal(reader);
                        break;
                    }

                case TypeCode.Byte:
                    {
                        if (available >= 1) return reader.ReadByte();
                        break;
                    }
                case TypeCode.SByte:
                    {
                        if (available >= 1) return reader.ReadSByte();
                        break;
                    }

                case TypeCode.Empty:
                case TypeCode.Object:
                case TypeCode.DBNull:
                case TypeCode.Char:
                case TypeCode.DateTime:
                case TypeCode.String:
                    throw new Exception($"Type \"{type}\" is not supported!");

                default: throw new Exception($"Type \"{type}\" is not supported!");
            }
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static decimal ReadDecimal(BinaryReader reader)
        {
            var low = reader.ReadUInt64();
            var high = reader.ReadUInt32();
            var signScale = reader.ReadUInt32();

            int lo = (int)(low & 0xFFFFFFFFL),
                mid = (int)((low >> 32) & 0xFFFFFFFFL),
                hi = (int)high;
            var isNeg = (signScale & 0x0001) == 0x0001;
            var scale = (byte)((signScale & 0x01FE) >> 1);
            return new decimal(lo, mid, hi, isNeg, scale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void WriteDecimal(BinaryWriter writer, decimal value)
        {
            var res = new byte[16];
            var bits = decimal.GetBits(value);
            ulong a = (ulong)bits[1] << 32, b = (ulong)bits[0] & 0xFFFFFFFFL;
            var low = a | b;
            var high = (uint)bits[2];
            var signScale = (uint)(((bits[3] >> 15) & 0x01FE) | ((bits[3] >> 31) & 0x0001));

            if (low != 0)
                Buffer.BlockCopy(low.GetBytes(), 0, res, 0, 8);
            if (high != 0)
                Buffer.BlockCopy(high.GetBytes(), 0, res, 8, 4);
            if (signScale != 0)
                Buffer.BlockCopy(signScale.GetBytes(), 0, res, 12, 4);
            writer.Write(res);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static decimal ReadDecimal(byte[] data)
        {
            var low = BitConverter.ToUInt64(data, 0);
            var high = BitConverter.ToUInt32(data, 8);
            var signScale = BitConverter.ToUInt64(data, 12);

            int lo = (int)(low & 0xFFFFFFFFL),
                mid = (int)((low >> 32) & 0xFFFFFFFFL),
                hi = (int)high;
            var isNeg = (signScale & 0x0001) == 0x0001;
            var scale = (byte)((signScale & 0x01FE) >> 1);
            return new decimal(lo, mid, hi, isNeg, scale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static byte[] WriteDecimal(decimal value)
        {
            var res = new byte[16];
            var bits = decimal.GetBits(value);
            ulong a = (ulong)bits[1] << 32, b = (ulong)bits[0] & 0xFFFFFFFFL;
            var low = a | b;
            var high = (uint)bits[2];
            var signScale = (uint)(((bits[3] >> 15) & 0x01FE) | ((bits[3] >> 31) & 0x0001));

            if (low != 0)
                Buffer.BlockCopy(low.GetBytes(), 0, res, 0, 8);
            if (high != 0)
                Buffer.BlockCopy(high.GetBytes(), 0, res, 8, 4);
            if (signScale != 0)
                Buffer.BlockCopy(signScale.GetBytes(), 0, res, 12, 4);
            return res;
        }
    }
}