using System;

namespace BinarySilvelizerX.Attributes
{
    /// <inheritdoc />
    /// <summary>
    /// Used in conjunction with one or more Subtype attributes to specify the default type to use during deserialization.
    /// You also can use two default subtypes with different targets.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class BFSubtypeDefaultAttribute : Attribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of <see cref="T:BinarySilvelizerX.Attributes.BFSubtypeDefaultAttribute" />.
        /// </summary>
        /// <param name="subtype">The default subtype.</param>
        /// <param name="target">Primary subtype target to be used.</param>
        public BFSubtypeDefaultAttribute(Type subtype, SubtypeBindingTarget target = SubtypeBindingTarget.Unspecified)
        {
            Subtype = subtype;
            Target = target;
        }

        /// <summary>
        ///     The default subtype.  This type must be assignable to the field type.
        /// </summary>
        public Type Subtype { get; }

        /// <summary>
        ///     The subtype target.
        /// </summary>
        public SubtypeBindingTarget Target { get; }
    }
}