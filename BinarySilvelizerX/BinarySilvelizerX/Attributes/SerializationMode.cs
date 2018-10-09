using System;
using BinarySilvelizerX.Core;

// ReSharper disable UnusedMember.Global

namespace BinarySilvelizerX.Attributes
{
    /// <inheritdoc />
    /// <summary>
    /// This attribute can be used to define which properties will be automatically (de-)serialized.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public sealed class SerializationModeAttribute : Attribute
    {
        public string PropertyName { get; }
        public SerializationAccessMode AccessMode { get; }
        public SerializationOffsetMode OffsetMode { get; }
        public SerializationAccessorMode AccessorMode { get; }

        /// <inheritdoc />
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
            AccessorMode = SerializerDefaults.DefaultAccessorMode;
            AccessMode = accessMode;
            OffsetMode = offsetMode;
            PropertyName = propName;
        }

        /// <inheritdoc />
        /// <param name="accessMode">Class (or structure) serialization mode by property access flag</param>
        /// <param name="accessorMode">Class (or structure) serialization mode by property accessor type</param>
        /// <param name="offsetMode">Class (or structure) serialization mode by property offset</param>
        /// <param name="propName">Property name which will be used as serialization mode argument</param>
        public SerializationModeAttribute(SerializationAccessMode accessMode,
            SerializationAccessorMode accessorMode,
            SerializationOffsetMode offsetMode = SerializationOffsetMode.Unrestricted,
            string propName = null)
        {
            if (offsetMode != SerializationOffsetMode.Unrestricted && string.IsNullOrEmpty(propName))
                throw new ArgumentException("Property name in serialization offset mode cannot be null or empty!",
                    nameof(propName));
            AccessorMode = accessorMode;
            AccessMode = accessMode;
            OffsetMode = offsetMode;
            PropertyName = propName;
        }

        /// <inheritdoc />
        /// <param name="offsetMode">Class (or structure) serialization mode by property offset</param>
        /// <param name="propName">Property name which will be used as serialization mode argument</param>
        public SerializationModeAttribute(SerializationOffsetMode offsetMode, string propName)
        {
            if (string.IsNullOrEmpty(propName))
                throw new ArgumentException("Property name in serialization offset mode cannot be null or empty!",
                    nameof(propName));
            AccessorMode = SerializerDefaults.DefaultAccessorMode;
            AccessMode = SerializerDefaults.DefaultPropAccessMode;
            OffsetMode = offsetMode;
            PropertyName = propName;
        }

        /// <inheritdoc />
        /// <param name="accessorMode">Class (or structure) serialization mode by property accessor type</param>
        /// <param name="offsetMode">Class (or structure) serialization mode by property offset</param>
        /// <param name="propName">Property name which will be used as serialization mode argument</param>
        public SerializationModeAttribute(SerializationAccessorMode accessorMode,
            SerializationOffsetMode offsetMode = SerializationOffsetMode.Unrestricted,
            string propName = null)
        {
            if (offsetMode != SerializationOffsetMode.Unrestricted && string.IsNullOrEmpty(propName))
                throw new ArgumentException("Property name in serialization offset mode cannot be null or empty!",
                    nameof(propName));
            AccessorMode = accessorMode;
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

    public enum SerializationAccessorMode
    {
        Unrestricted,
        OnlyReadable,
        OnlyWritable,
        OnlyBoth
    }
}