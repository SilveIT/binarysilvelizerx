using System.Reflection;
using BinarySilvelizerX.Common;
using BinarySilvelizerX.Core;
using BinarySilvelizerX.Streams;

namespace BinarySilvelizerX.SerializerNodes
{
    internal class ObjectNode : BasicNode
    {
        internal SubtypeInfo SubtypeInfo { get; }

        public ObjectNode(PropertyInfo info, SubtypeInfo subtypeInfo) : base(info, NodeType.Object) => SubtypeInfo = subtypeInfo;

        internal override void Serialize(ExtendedWriter writer, object sourceObject)
        {
            var existingObj = Info.GetValue(sourceObject);
            var subType = SubtypeInfo.GetSerializationSubtype(sourceObject);
            Serializer.Serialize(writer, existingObj, subType ?? Info.PropertyType);
        }

        internal override bool Deserialize(ExtendedReader reader, object targetObject)
        {
            var existingObj = Info.GetValue(targetObject);
            var subType = SubtypeInfo.GetDeserializationSubtype(targetObject);
            Info.SetValue(targetObject, Deserializer.Deserialize(reader, subType ?? Info.PropertyType, existingObj));
            return true;
        }
    }
}