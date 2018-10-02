using System.Text;
using BinarySilvelizerX.Attributes;

namespace BinarySilvelizerX.Core
{
    public static class SerializerDefaults
    {
        public static Encoding DefaultStringEncoding { get; set; } = Encoding.UTF8;
        public static SerializationAccessMode DefaultPropAccessMode { get; set; } = SerializationAccessMode.AllPublic;
        public static bool ThrowIfNoSerializableNodesFound { get; set; } = true;
    }
}