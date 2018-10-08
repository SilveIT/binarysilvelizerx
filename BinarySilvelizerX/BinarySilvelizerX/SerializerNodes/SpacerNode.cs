using System.Reflection;
using BinarySilvelizerX.Attributes;
using BinarySilvelizerX.Extensions;
using BinarySilvelizerX.Streams;
using BinarySilvelizerX.Utils;

namespace BinarySilvelizerX.SerializerNodes
{
    internal class SpacerNode : BasicNode
    {
        internal int SpacerLength { get; }

        public SpacerNode(PropertyInfo info) : base(info, NodeType.Spacer)
        {
            SpacerLength = info.GetFirstAttribute<BFSpacerAttribute>().SpacerLength; //TODO: mb replace it with BFLength?
        }

        internal override void Serialize(ExtendedWriter writer, object sourceObject) => writer.Write(BufferHelper.GetBytes(SpacerLength));

        internal override bool Deserialize(ExtendedReader reader, object targetObject)
        {
            if (reader.AvailableLength() < SpacerLength) return false;
            reader.ReadBytes(SpacerLength);
            return true;
        }
    }
}