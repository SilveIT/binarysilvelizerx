using System;
using BinarySilvelizerX.Interfaces;

namespace BinarySilvelizerX.Attributes
{
    public class FieldBindingAttribute : Attribute, IFieldBinding
    {
        public string BindingPath { get; set; }
    }
}