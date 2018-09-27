using System;
using System.Collections;
using System.Collections.Generic;
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
    internal class ListNode : SizableNode
    {
        private readonly LengthInfo _lengthInfo;

        public ListNode(PropertyInfo info, LengthInfo lengthInfo) : base(info, NodeType.List, lengthInfo)
        {
            _lengthInfo = lengthInfo;
        }

        internal override void Serialize(ExtendedWriter writer, object sourceObject)
        {
            var type = Info.PropertyType;
            var listSubType = type.GetElementType() ?? type.GetGenericArguments().Single();
            var list = (IList)Info.GetValue(sourceObject);
            int len;
            if (_lengthInfo.StorageType == LengthStorageType.Dynamic)
            {
                len = list?.Count ?? 0;
                var lnType = _lengthInfo.LengthType;
                writer.Write(ValueTypeSerializer.Write(len, lnType));
                if (len == 0) return;
                if (list == null)
                    throw new Exception("Something goes wrong... List can't be null there!"); //It won't be called anyway.. I hope...
            }
            else
            {
                len = GetLength(sourceObject);
                if (len == 0) return;
                var listCnt = list?.Count ?? 0;
                if (listCnt != len)
                {
                    if (listSubType != typeof(byte) && listCnt < len)
                        throw new NotImplementedException("Extending non byte list is not supported this moment!");
                    if (list == null)
                        list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listSubType));
                    list = GetFitList(list, len, listSubType);
                }
                if (list == null)
                    throw new Exception("Something goes wrong... List can't be null there!");
            }

            if (listSubType == typeof(byte))
            {
                var arr = new byte[len];
                list.CopyTo(arr, 0);
                writer.Write(arr);
            }
            else
            {
                var cntr = 0;
                foreach (var item in list)
                {
                    if (cntr >= len) break;
                    Serializer.Serialize(writer, item, listSubType);
                    cntr++;
                }
            }
        }

        internal override bool Deserialize(ExtendedReader reader, object targetObject)
        {
            var type = Info.PropertyType;
            var listSubType = type.GetElementType() ?? type.GetGenericArguments().Single();
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
            if (listSubType != typeof(byte))
            {
                var list = (IList)Info.GetValue(targetObject);
                var propValLen = list?.Count ?? -1;
                if (list == null || propValLen != len) //if length isn's same we will override it with new collection
                    list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listSubType));

                for (var o = 0; o < len; o++)
                    list.Add(Deserializer.Deserialize(reader, listSubType, null));

                Info.SetValue(targetObject, list);
            }
            else if (reader.BaseStream.AvailableLength() >= len)
                Info.SetValue(targetObject, reader.ReadBytes(len).ToList());
            else
                return false;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IList GetFitList(IList list, int len, Type listSubType)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (list.Count == len) return list;
            //TODO mb fill it with new instances?
            if (list.Count > len)
                for (var i = list.Count; i > len; i--)
                    list.RemoveAt(i - 1);
            else
            {
                if (listSubType != typeof(byte))
                    throw new NotImplementedException("Extending non byte list is not supported this moment!");
                for (var i = list.Count; i > len; i--)
                    list.Add((byte)0);
            }
            return list;
        }
    }
}