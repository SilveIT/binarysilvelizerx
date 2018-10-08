using System;
using BinarySilvelizerX.Interfaces;

namespace BinarySilvelizerX.Attributes
{
    /// <summary>
    /// Used to denote the type of a subtype factory object that implements ISubtypeFactory. You can use an unlimited number of factories.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class BFSubtypeFactoryAttribute : FieldBindingAttribute
    {
        internal ISubtypeFactory Factory { get; }
        internal SubtypeBindingTarget Target { get; }

        /// <summary>
        ///     Initializes a new instance of <see cref="BFSubtypeFactoryAttribute" />.
        /// </summary>
        /// <param name="propName">The path to the binding source.</param>
        /// <param name="factoryType">The type of subtype factory to be used.</param>
        /// <param name="target">Primary subtype factory target to be used.</param>
        public BFSubtypeFactoryAttribute(string propName, Type factoryType, SubtypeBindingTarget target = SubtypeBindingTarget.Unspecified)
        {
            if (string.IsNullOrEmpty(propName))
                throw new ArgumentException("Property name cannot be null or empty!", nameof(propName));
            if (factoryType == null)
                throw new ArgumentException("Factory type cannot be null!", nameof(factoryType));
            if (!typeof(ISubtypeFactory).IsAssignableFrom(factoryType))
                throw new ArgumentException("Factory type must implement ISubtypeFactory", nameof(factoryType));
            var instance = (ISubtypeFactory)factoryType.GetConstructor(new Type[0])?.Invoke(null);
            Factory = instance ?? throw new ArgumentException("Cannot create factory instance!", nameof(factoryType));
            BindingPath = propName;
            Target = target;
        }
    }
}