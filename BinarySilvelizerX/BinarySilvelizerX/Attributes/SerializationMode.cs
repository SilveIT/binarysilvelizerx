using System;
using BinarySilvelizerX.Core;

namespace BinarySilvelizerX.Attributes
{
    /// <summary>
    /// This attribute can be used to define which properties will be automatically (de-)serialized.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public sealed class SerializationModeAttribute : Attribute
    {
        public string PropertyName { get; }
        public SerializationAccessMode AccessMode { get; }
        public SerializationOffsetMode OffsetMode { get; }

        /// <param name="accessMode">Class (or structure) serialization mode by property access flag</param>
        /// <param name="offsetMode">Class (or structure) serialization mode by property offset</param>
        /// <param name="propName">Property name which will be used as serialization mode argument</param>

        public SerializationModeAttribute(SerializationAccessMode accessMode,
            SerializationOffsetMode offsetMode = SerializationOffsetMode.Unrestricted,
            string propName = null)
        {
            if (offsetMode != SerializationOffsetMode.Unrestricted && string.IsNullOrEmpty(propName))
                throw new ArgumentException("Property name in serialization offset mode cannot be null or empty!",
                    nameof(propName));
            AccessMode = accessMode;
            OffsetMode = offsetMode;
            PropertyName = propName;
        }

        public SerializationModeAttribute(SerializationOffsetMode offsetMode, string propName)
        {
            if (string.IsNullOrEmpty(propName))
                throw new ArgumentException("Property name in serialization offset mode cannot be null or empty!",
                    nameof(propName));
            AccessMode = SerializerDefaults.DefaultPropAccessMode;
            OffsetMode = offsetMode;
            PropertyName = propName;
        }
    }

    public enum SerializationOffsetMode
    {
        Unrestricted,
        StartingFrom,
        EndingOn
    }

    [Flags]
    public enum SerializationAccessMode
    {
        OnlyByteFields = 0,
        AllPublic = 1 << 0,
        AllPrivate = 1 << 1,
        All = AllPublic | AllPrivate
    }
}