using System;

// ReSharper disable UnusedMember.Global

namespace BinarySilvelizerX.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class BFLengthAttribute : FieldBindingAttribute
    {
        internal int StaticLength { get; } = -1;
        internal LengthStorageType LengthStorageType { get; }

        /// <summary>
        /// Use this constructor to (de-)serialize static length collection/string without writing (and reading) length.
        /// </summary>
        /// <param name="staticLength">Static length value</param>
        public BFLengthAttribute(int staticLength)
        {
            if (staticLength < 0)
                throw new ArgumentException("Static length cannot be less than zero!", nameof(staticLength));
            StaticLength = staticLength;
            LengthStorageType = LengthStorageType.Static;
        }

        /// <summary>
        /// Use this constructor to (de-)serialize dynamic size collection/string with external place to store/read it's length.
        /// </summary>
        /// <param name="propName">Property which will be used in length (de-)serialization</param>
        public BFLengthAttribute(string propName)
        {
            if (string.IsNullOrEmpty(propName))
                throw new ArgumentException("Property name cannot be null or empty!", nameof(propName));
            BindingPath = propName;
            LengthStorageType = LengthStorageType.External;
        }
    }

    internal enum LengthStorageType
    {
        Dynamic,
        Static,
        External
    }
}