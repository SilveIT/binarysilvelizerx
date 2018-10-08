using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using BinarySilvelizerX.Attributes;
using BinarySilvelizerX.Interfaces;
using BinarySilvelizerX.SerializerNodes;

namespace BinarySilvelizerX.Common
{
    internal class SubtypeInfo
    {
        private readonly List<ISubtypeToken> _serializationTokens = new List<ISubtypeToken>();
        private readonly List<ISubtypeToken> _deserializationTokens = new List<ISubtypeToken>();

        internal SubtypeInfo(ICustomAttributeProvider prop, IReadOnlyCollection<BasicNode> allNodes)
        {
            var attributes = prop.GetCustomAttributes(false); //TODO think about inheriting

            var subtypeTokens = new List<ISubtypeToken>();
            var subtypeFactoryTokens = new List<ISubtypeToken>();
            var subtypeDefaultTokens = new List<ISubtypeToken>(2);

            foreach (var attr in attributes)
            {
                var attrType = attr.GetType();
                if (attrType != typeof(BFSubtypeDefaultAttribute))
                {
                    var bondedNode = allNodes.First(t => t.Name == ((FieldBindingAttribute)attr).BindingPath);
                    if (attrType == typeof(BFSubtypeAttribute))
                    {
                        var cAttr = (BFSubtypeAttribute)attr;
                        subtypeTokens.Add(new SubtypeToken(bondedNode,
                            cAttr.Subtype, cAttr.Value, cAttr.Target));
                    }
                    else if (attrType == typeof(BFSubtypeFactoryAttribute))
                    {
                        var cAttr = (BFSubtypeFactoryAttribute)attr;
                        subtypeFactoryTokens.Add(new SubtypeFactoryToken(bondedNode, cAttr.Factory, cAttr.Target));
                    }
                }
                else
                {
                    var cAttr = (BFSubtypeDefaultAttribute)attr;
                    subtypeDefaultTokens.Add(new SubtypeDefaultToken(cAttr.Subtype, cAttr.Target));
                }
            }

            FillWithTokens(subtypeTokens.OrderByDescending(t => (int)t.Target));
            FillWithTokens(subtypeFactoryTokens.OrderByDescending(t => (int)t.Target));
            FillWithTokens(subtypeDefaultTokens.OrderByDescending(t => (int)t.Target));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void FillWithTokens(IEnumerable<ISubtypeToken> tokens)
        {
            foreach (var token in tokens)
            {
                switch (token.Target)
                {
                    case SubtypeBindingTarget.Unspecified:
                        _serializationTokens.Add(token);
                        _deserializationTokens.Add(token);
                        break;

                    case SubtypeBindingTarget.Serialization:
                        _serializationTokens.Add(token);
                        break;

                    case SubtypeBindingTarget.Deserialization:
                        _deserializationTokens.Add(token);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Type GetSerializationSubtype(object sourceObject)
        {
            Type type = null;
            for (var i = 0; i < _serializationTokens.Count && type == null; i++)
                type = _serializationTokens[i].GetValue(sourceObject);
            return type;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Type GetDeserializationSubtype(object sourceObject)
        {
            Type type = null;
            for (var i = 0; i < _deserializationTokens.Count && type == null; i++)
                type = _deserializationTokens[i].GetValue(sourceObject);
            return type;
        }
    }

    internal class SubtypeToken : ISubtypeToken
    {
        internal BasicNode Source { get; }

        public object Value { get; }

        public Type Subtype { get; }

        public SubtypeBindingTarget Target { get; }

        internal SubtypeToken(BasicNode source, Type subType, object value, SubtypeBindingTarget target)
        {
            Source = source;
            Subtype = subType;
            Value = value;
            Target = target;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Type GetValue(object sourceObject) => Equals(Source.Info.GetValue(sourceObject), Value) ? Subtype : null;
    }

    internal class SubtypeFactoryToken : ISubtypeToken
    {
        internal BasicNode Source { get; }

        public SubtypeBindingTarget Target { get; }
        internal ISubtypeFactory Factory { get; }

        internal SubtypeFactoryToken(BasicNode source, ISubtypeFactory factory, SubtypeBindingTarget target)
        {
            Source = source;
            Target = target;
            Factory = factory;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Type GetValue(object sourceObject)
        {
            Factory.TryGetType(Source.Info.GetValue(sourceObject), out var type);
            return type;
        }
    }

    internal class SubtypeDefaultToken : ISubtypeToken
    {
        public SubtypeBindingTarget Target { get; }
        public Type Subtype { get; }

        internal SubtypeDefaultToken(Type subType, SubtypeBindingTarget target)
        {
            Subtype = subType;
            Target = target;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Type GetValue(object sourceObject) => Subtype;
    }
}