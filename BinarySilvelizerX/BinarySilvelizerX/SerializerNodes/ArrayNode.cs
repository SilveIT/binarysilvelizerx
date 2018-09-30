using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using BinarySilvelizerX.Attributes;
using BinarySilvelizerX.Common;
using BinarySilvelizerX.Core;
using BinarySilvelizerX.Extensions;
using BinarySilvelizerX.PrimitiveSerializers;
using BinarySilvelizerX.Streams;

namespace BinarySilvelizerX.SerializerNodes
{
    internal class ArrayNode : SizableNode
    {
        private readonly LengthInfo _lengthInfo;

        public ArrayNode(PropertyInfo info, LengthInfo lengthInfo) : base(info, NodeType.Array, lengthInfo)
        {
            _lengthInfo = lengthInfo;
        }

        internal override void Serialize(ExtendedWriter writer, object sourceObject)
        {
            var type = Info.PropertyType;
            var arraySubType = type.GetElementType() ?? type.GetGenericArguments().Single();
            var array = (Array)Info.GetValue(sourceObject);
            int len;
            if (_lengthInfo.StorageType == LengthStorageType.Dynamic)
            {
                len = array?.Length ?? 0;
                var lnType = _lengthInfo.LengthType;
                writer.Write(ValueTypeSerializer.Write(len, lnType));
                if (len == 0) return;
                if (array == null)
                    throw new Exception("Something goes wrong... Array can't be null there!"); //It won't be called anyway.. I hope...
            }
            else
            {
                len = GetLength(sourceObject);
                if (len == 0) return;
                var arrLen = array?.Length ?? 0;
                if (arrLen != len)
                {
                    if (arraySubType != typeof(byte) && arrLen < len)
                        throw new Exception("Extending non byte array is not supported this moment!"); //TODO impl mb
                    if (array == null)
                        array = Array.CreateInstance(arraySubType, len);
                    array = GetFitArray(array, len, arraySubType);
                }
                if (array == null)
                    throw new Exception("Something goes wrong... Array can't be null there!");
            }

            if (arraySubType == typeof(byte))
                writer.Write((byte[])array);
            else
            {
                var cntr = 0;
                foreach (var item in array)
                {
                    if (cntr >= len) break;
                    Serializer.Serialize(writer, item, arraySubType);
                    cntr++;
                }
            }
        }

        internal override bool Deserialize(ExtendedReader reader, object targetObject)
        {
            var type = Info.PropertyType;
            var arraySubType = type.GetElementType() ?? type.GetGenericArguments().Single();
            var lnType = _lengthInfo.LengthType;
            int len;
            if (_lengthInfo.StorageType == LengthStorageType.Dynamic)
            {
                var l = ValueTypeSerializer.Read(reader, lnType);
                if (l == null) return false;
                len = (int)l;
            }
            else
                len = GetLength(targetObject);
            if (arraySubType != typeof(byte))
            {
                var array = (Array)Info.GetValue(targetObject);
                var propValLen = array?.Length ?? -1;
                if (array == null || propValLen != len) //if length isn's same we will override it with new array
                    array = Array.CreateInstance(arraySubType, len);

                for (var o = 0; o < len; o++)
                    array.SetValue(Deserializer.Deserialize(reader, arraySubType, null), o);

                Info.SetValue(targetObject, array);
            }
            else if (reader.AvailableLength() >= len)
                Info.SetValue(targetObject, reader.ReadBytes(len));
            else
                return false;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Array GetFitArray(Array arr, int length, Type arrSubType)
        {
            if (arr.Length == length) return arr;
            int copyLen;
            if (arr.Length > length)
            {
                Logger.Write("Defined array length < real array length, cutting...", "O2B", Logger.MessageType.Info);
                copyLen = length;
            }
            else copyLen = arr.Length; //extending in this case
            var outp = Array.CreateInstance(arrSubType, length);
            if (copyLen != 0)
                Array.Copy(arr, 0, outp, 0, copyLen);
            return outp;
            //return new ArraySegment<object>(arr, 0, copyLen).ToArray();
        }
    }
}