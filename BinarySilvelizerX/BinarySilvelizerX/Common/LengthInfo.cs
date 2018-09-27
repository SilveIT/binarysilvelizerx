using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BinarySilvelizerX.Attributes;
using BinarySilvelizerX.SerializerNodes;
using BinarySilvelizerX.Extensions;

namespace BinarySilvelizerX.Common
{
    internal class LengthInfo
    {
        internal ValueTypeNode LengthSource { get; }
        internal Type LengthType { get; }
        internal LengthStorageType StorageType { get; }
        internal int StaticLength { get; }

        internal LengthInfo(PropertyInfo prop, IEnumerable<BasicNode> allProps)
        {
            var lenTypeAttr = prop.GetFirstAttribute<BFLengthTypeAttribute>();
            LengthType = lenTypeAttr?.Type ?? typeof(int);
            var lenAttr = prop.GetFirstAttribute<BFLengthAttribute>();
            if (lenAttr != null)
                StorageType = lenAttr.LengthStorageType;
            switch (StorageType)
            {
                case LengthStorageType.Dynamic:
                    break;

                case LengthStorageType.Static:
                    // ReSharper disable once PossibleNullReferenceException
                    StaticLength = lenAttr.StaticLength; //-V3125
                    break;

                case LengthStorageType.External:
                    // ReSharper disable once PossibleNullReferenceException
                    LengthSource = allProps.First(t => t.Type == NodeType.ValueType && t.Name == lenAttr.LengthPropertyName) as ValueTypeNode;
                    if (LengthSource == null)
                        throw new Exception($"Unable to find LengthSource of {prop.Name}! Make sure exists in the serialized properties before current!");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}