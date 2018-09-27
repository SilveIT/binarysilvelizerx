using System;
using System.Reflection;
using BinarySilvelizerX.PrimitiveSerializers;
using BinarySilvelizerX.Streams;

namespace BinarySilvelizerX.SerializerNodes
{
    internal class ValueTypeNode : BasicNode
    {
        private readonly Type _valueType;

        public ValueTypeNode(PropertyInfo info, Type valueType)
            : base(info, NodeType.ValueType) => _valueType = valueType;

        internal override void Serialize(ExtendedWriter writer, object sourceObject) => ValueTypeSerializer.Write(writer, Info.GetValue(sourceObject), _valueType);

        internal override bool Deserialize(ExtendedReader reader, object targetObject)
        {
            var val = ValueTypeSerializer.Read(reader, _valueType);
            if (val == null) return false;
            Info.SetValue(targetObject, val);
            return true;
        }
    }
}