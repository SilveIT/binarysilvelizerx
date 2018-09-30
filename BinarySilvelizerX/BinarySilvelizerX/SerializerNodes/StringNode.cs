using System;
using System.Reflection;
using System.Text;
using BinarySilvelizerX.Attributes;
using BinarySilvelizerX.Common;
using BinarySilvelizerX.Core;
using BinarySilvelizerX.Extensions;
using BinarySilvelizerX.PrimitiveSerializers;
using BinarySilvelizerX.Streams;

namespace BinarySilvelizerX.SerializerNodes
{
    internal class StringNode : SizableNode
    {
        private readonly LengthInfo _lengthInfo;
        private readonly Encoding _stringEncoding;

        public StringNode(PropertyInfo info, LengthInfo lengthInfo) : base(info, NodeType.String, lengthInfo)
        {
            _lengthInfo = lengthInfo;
            _stringEncoding = Info.GetFirstAttribute<BFEncodingAttribute>()?.Encoding
                ?? SerializerDefaults.DefaultStringEncoding;
        }

        internal override void Serialize(ExtendedWriter writer, object sourceObject)
        {
            var lnType = _lengthInfo.LengthType;
            var objAsString = Info.GetValue(sourceObject) as string;
            var enc = _stringEncoding;
            var bytesPerChar = enc.GetByteCount("0");
            var dataLength = bytesPerChar; //null + terminal null
            byte[] data;
            if (string.IsNullOrEmpty(objAsString))
                data = null;
            else
            {
                if (_lengthInfo.StorageType != LengthStorageType.Dynamic) //TODO optimize
                {
                    var staticLen = GetLength(sourceObject);
                    if (objAsString.Length > staticLen)
                    {
                        Logger.Write("String length > defined static string length, cutting!", "O2B",
                            Logger.MessageType.Info);
                        objAsString = objAsString.Substring(0, staticLen);
                    }
                    data = enc.GetBytes(objAsString);
                    dataLength += staticLen * bytesPerChar;
                }
                else
                {
                    data = enc.GetBytes(objAsString);
                    dataLength += data.Length;
                }
            }

            if (_lengthInfo.StorageType == LengthStorageType.Dynamic)
            {
                var len = (objAsString?.Length ?? 0) + 1; //len + terminal null
                writer.Write(ValueTypeSerializer.Write(len, lnType));
            }

            var buf = new byte[dataLength];
            data?.CopyTo(buf, 0);
            writer.Write(buf);
        }

        internal override bool Deserialize(ExtendedReader reader, object targetObject)
        {
            var lnType = _lengthInfo.LengthType;
            var enc = _stringEncoding;
            var bytesPerChar = enc.GetByteCount("0");
            int len;
            if (_lengthInfo.StorageType == LengthStorageType.Dynamic)
            {
                var intType = typeof(int);
                var gotLen = ValueTypeSerializer.Read(reader, lnType);
                len = lnType == intType ? (int)gotLen : (int)Convert.ChangeType(gotLen, intType);
            }
            else
                len = GetLength(targetObject) + 1;

            if (bytesPerChar != 1)
                len *= bytesPerChar;
            var str = string.Empty;
            if (len > 0)
            {
                if (reader.AvailableLength() < len) return false;
                str = enc.GetString(reader.ReadBytes(len)).TrimEnd('\0');
            }
            Info.SetValue(targetObject, str);
            return true;
        }
    }
}