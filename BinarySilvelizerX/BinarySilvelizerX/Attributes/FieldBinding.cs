using System;

namespace BinarySilvelizerX.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldBindingAttribute : Attribute
    {
        /// <summary>
        ///     Gets or sets the path to the binding source member.
        /// </summary>
        public string BindingPath { get; set; }
    }
}