using System.Reflection;
using BinarySilvelizerX.Core;
using BinarySilvelizerX.Streams;

namespace BinarySilvelizerX.SerializerNodes
{
    internal class ObjectNode : BasicNode
    {
        public ObjectNode(PropertyInfo info) : base(info, NodeType.Object)
        {
        }

        internal override void Serialize(ExtendedWriter writer, object sourceObject)
        {
            var obj = Info.GetValue(sourceObject);
            Serializer.Serialize(writer, obj, Info.PropertyType);
        }

        internal override bool Deserialize(ExtendedReader reader, object targetObject)
        {
            var propVal = Info.GetValue(targetObject);
            Info.SetValue(targetObject, Deserializer.Deserialize(reader, Info.PropertyType, propVal));
            return true;
        }
    }
}