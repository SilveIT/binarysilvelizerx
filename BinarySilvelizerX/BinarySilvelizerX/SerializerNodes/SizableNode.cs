using System;
using System.Reflection;
using BinarySilvelizerX.Attributes;
using BinarySilvelizerX.Common;

namespace BinarySilvelizerX.SerializerNodes
{
    internal abstract class SizableNode : BasicNode
    {
        private readonly LengthInfo _lengthInfo;

        internal SizableNode(PropertyInfo info, NodeType type, LengthInfo lengthInfo) : base(info, type)
        {
            _lengthInfo = lengthInfo;
        }

        internal int GetLength(object sourceObject)
        {
            switch (_lengthInfo.StorageType)
            {
                case LengthStorageType.Dynamic:
                    return -1;

                case LengthStorageType.Static:
                    return _lengthInfo.StaticLength;

                case LengthStorageType.External:
                    var lenVal = _lengthInfo.LengthSource.Info.GetValue(sourceObject);
                    var intType = typeof(int);
                    return Info.PropertyType == intType
                        ? (int)lenVal
                        : (int)Convert.ChangeType(lenVal, intType);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal void SetLength(object sourceObject, int value)
        {
            if (_lengthInfo.StorageType == LengthStorageType.External)
            {
                var intType = typeof(int);
                _lengthInfo.LengthSource.Info.SetValue(sourceObject, Info.PropertyType == intType
                    ? value
                    : (int)Convert.ChangeType(value, intType));
            }
            else throw new ArgumentOutOfRangeException();
        }
    }
}