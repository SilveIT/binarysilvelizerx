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
                    return (int)_lengthInfo.LengthSource.Info.GetValue(sourceObject);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal void SetLength(object sourceObject, int value)
        {
            if (_lengthInfo.StorageType == LengthStorageType.External)
                _lengthInfo.LengthSource.Info.SetValue(sourceObject, value);
            else throw new ArgumentOutOfRangeException();
        }
    }
}