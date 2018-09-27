using System.Reflection;
using BinarySilvelizerX.Streams;

namespace BinarySilvelizerX.SerializerNodes
{
    internal abstract class BasicNode
    {
        internal string Name => Info.Name;
        internal PropertyInfo Info { get; }
        internal NodeType Type { get; }

        internal BasicNode(PropertyInfo info, NodeType type)
        {
            Info = info;
            Type = type;
        }

        internal abstract void Serialize(ExtendedWriter writer, object sourceObject);

        internal abstract bool Deserialize(ExtendedReader reader, object targetObject);
    }
}