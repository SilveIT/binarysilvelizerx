using System;
using BinarySilvelizerX.Interfaces;

namespace BinarySilvelizerX.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class BFSubtypeFactoryAttribute : FieldBindingAttribute
    {
        internal Type FactoryType { get; }

        public BFSubtypeFactoryAttribute(string propName, Type factoryType)
        {
            if (string.IsNullOrEmpty(propName))
                throw new ArgumentException("Property name cannot be null or empty!", nameof(propName));
            if (!typeof(ISubtypeFactory).IsAssignableFrom(factoryType))
                throw new ArgumentException("Factory type must implement ISubtypeFactory", nameof(factoryType));
            BindingPath = propName;
            FactoryType = factoryType;
        }
    }
}