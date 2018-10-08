using System;

namespace BinarySilvelizerX.Attributes
{
    /// <summary>
    ///     Used to specify multiple possible derived types.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class BFSubtypeAttribute : FieldBindingAttribute
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="BFSubtypeAttribute" />.
        /// </summary>
        /// <param name="valuePath">The path to the binding source.</param>
        /// <param name="value">The value to be used in determining if the subtype should be used.</param>
        /// <param name="subtype">The subtype to be used.</param>
        /// <param name="target">Primary subtype target to be used.</param>
        public BFSubtypeAttribute(string valuePath, object value, Type subtype, SubtypeBindingTarget target = SubtypeBindingTarget.Unspecified)
        {
            BindingPath = valuePath;
            Value = value;
            Subtype = subtype;
            Target = target;
        }

        /// <summary>
        ///     The value that defines the subtype mapping.
        /// </summary>
        public object Value { get; }

        /// <summary>
        ///     The subtype.
        /// </summary>
        public Type Subtype { get; }

        /// <summary>
        ///     The subtype target.
        /// </summary>
        public SubtypeBindingTarget Target { get; }
    }
}