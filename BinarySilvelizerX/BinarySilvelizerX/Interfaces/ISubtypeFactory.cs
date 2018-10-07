using System;

namespace BinarySilvelizerX.Interfaces
{
    public interface ISubtypeFactory
    {
        bool TryGetKey(Type valueType, out object key);

        bool TryGetType(object key, out Type type);
    }
}