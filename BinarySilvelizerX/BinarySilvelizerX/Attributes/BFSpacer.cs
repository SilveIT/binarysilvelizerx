using System;

namespace BinarySilvelizerX.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class BFSpacerAttribute : Attribute
    {
        public int SpacerLength { get; }

        public BFSpacerAttribute(int spacerLength)
        {
            SpacerLength = spacerLength;
        }
    }
}