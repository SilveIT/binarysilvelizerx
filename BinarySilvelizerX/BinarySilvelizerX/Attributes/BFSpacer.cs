using System;

namespace BinarySilvelizerX.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BFSpacerAttribute : Attribute
    {
        public int SpacerLength { get; }

        public BFSpacerAttribute(int spacerLength)
        {
            SpacerLength = spacerLength;
        }
    }
}