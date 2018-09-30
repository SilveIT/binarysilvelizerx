using System.Net;
using System.Reflection;
using BinarySilvelizerX.Extensions;
using BinarySilvelizerX.Streams;

namespace BinarySilvelizerX.SerializerNodes
{
    internal class IPAddressNode : BasicNode
    {
        public IPAddressNode(PropertyInfo info) : base(info, NodeType.IPAddress)
        {
        }

        internal override void Serialize(ExtendedWriter writer, object sourceObject)
        {
            var ip = (IPAddress)Info.GetValue(sourceObject);
            if (ip != null)
                writer.Write(ip.GetAddressBytes());
        }

        internal override bool Deserialize(ExtendedReader reader, object targetObject)
        {
            if (reader.AvailableLength() < 4) return false;
            Info.SetValue(targetObject, new IPAddress(reader.ReadBytes(4)));
            return true;
        }
    }
}