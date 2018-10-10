using System;
using System.IO;
using BinarySilvelizerX.Extensions;
using BinarySilvelizerX.PrimitiveSerializers;
using BinarySilvelizerX.Streams;

namespace BinarySilvelizerX.Core
{
    public static class Serializer
    {
        public static byte[] GetBytes<T>(this T source)
        {
            var type = source.GetType();
            return type.IsClearValueType()
                ? ValueTypeSerializer.Write(source)
                : (type == typeof(string) ? StringSerializer.WriteDynamic((string)(object)source) : Serialize(source, type));
        }

        private static byte[] Serialize<T>(T source, Type type)
        {
            var memStream = new MemoryStream();
            var writer = new ExtendedWriter(memStream);
            var nodes = NodeListController.GetNodes(type);
            if (nodes == null) return null;
            foreach (var node in nodes)
            {
                if (node.Info.CanRead)
                    node.Serialize(writer, source);
            }
            return memStream.ToArray();
        }

        internal static void Serialize<T>(ExtendedWriter writer, T source, Type type)
        {
            if (type.IsClearValueType())
            {
                ValueTypeSerializer.Write(writer, source, type);
                return;
            }
            if (type == typeof(string))
            {
                StringSerializer.WriteDynamic(writer, (string)(object)source);
            }
            var nodes = NodeListController.GetNodes(type);
            if (nodes == null) return;
            foreach (var node in nodes)
            {
                if (node.Info.CanRead)
                    node.Serialize(writer, source);
            }
        }
    }

    public static class Deserializer
    {
        public static T GetObject<T>(this byte[] data, object existingObject = null) =>
            (T)Deserialize(data, typeof(T), existingObject);

        private static object Deserialize(byte[] data, Type type, object existingObject)
        {
            if (type.IsClearValueType())
                return ValueTypeSerializer.Read(data, type);
            if (type == typeof(string))
                return StringSerializer.ReadDynamic(data);
            var memStream = new MemoryStream(data);
            var reader = new ExtendedReader(memStream);
            return Deserialize(reader, type, existingObject, true);
        }

        internal static object Deserialize(ExtendedReader reader, Type type, object existingObject, bool typeChecked = false)
        {
            if (!typeChecked)
            {
                if (type.IsClearValueType())
                    return ValueTypeSerializer.Read(reader, type);
                if (type == typeof(string))
                    return StringSerializer.ReadDynamic(reader);
            }

            var targetObject = existingObject ?? Activator.CreateInstance(type);
            var nodes = NodeListController.GetNodes(type);
            if (nodes == null) return targetObject;
            foreach (var node in nodes)
            {
                if (node.Info.CanWrite)
                    node.Deserialize(reader, targetObject);
            }
            return targetObject;
        }
    }
}