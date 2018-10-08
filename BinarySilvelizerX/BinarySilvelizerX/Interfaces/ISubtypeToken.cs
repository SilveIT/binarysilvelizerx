using System;
using BinarySilvelizerX.Attributes;

namespace BinarySilvelizerX.Interfaces
{
    public interface ISubtypeToken
    {
        Type GetValue(object sourceObject);

        SubtypeBindingTarget Target { get; }
    }
}