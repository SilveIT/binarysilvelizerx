using System;
using System.Reflection;
using BinarySilvelizerX.Attributes;
using BinarySilvelizerX.Common;

namespace BinarySilvelizerX.SerializerNodes
{
    internal abstract class SizableNode : BasicNode
    {
        internal LengthInfo LengthInfo { get; }

        internal SizableNode(PropertyInfo info, NodeType type, LengthInfo lengthInfo) : base(info, type) => LengthInfo = lengthInfo;

        internal int GetLength(object sourceObject)
        {
            switch (LengthInfo.StorageType)
            {
                case LengthStorageType.Dynamic:
                    return -1;

                case LengthStorageType.Static:
                    return LengthInfo.StaticLength;

                case LengthStorageType.External:
                    var lenVal = LengthInfo.LengthSource.Info.GetValue(sourceObject);
                    var intType = typeof(int);
                    return Info.PropertyType == intType
                        ? (int)lenVal
                        : (int)Convert.ChangeType(lenVal, intType);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        //internal void SetLength(object sourceObject, int value)
        //{
        //    if (LengthInfo.StorageType == LengthStorageType.External)
        //        LengthInfo.LengthSource.Info.SetValue(sourceObject, Info.PropertyType == typeof(int)
        //            ? value
        //            : Convert.ChangeType(value, Info.PropertyType));
        //    else throw new ArgumentOutOfRangeException();
        //}
    }
}