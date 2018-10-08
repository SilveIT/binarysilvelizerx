using System;
using System.Net;
using BinarySilvelizerX.Attributes;
using BinarySilvelizerX.Core;
using BinarySilvelizerX.Interfaces;

// ReSharper disable UnusedMember.Global

namespace BinarySilvelizerX.Example
{
    public class PacketFactory : ISubtypeFactory
    {
        public bool TryGetType(object key, out Type type)
        {
            if (Equals(key, Opcodes.Packet3))
            {
                type = typeof(FactoryDerModel);
                return true;
            }

            if (Equals(key, Opcodes.Packet4))
            {
                type = typeof(FactoryDerModel);
                return true;
            }
            type = null;
            return false;
        }
    }

    public class UniversalPacket : ByteModel<UniversalPacket>
    {
        public Opcodes Opcode { get; set; }

        [BFSubtype(nameof(Opcode), Opcodes.Packet1, typeof(DerivedModel1))]
        [BFSubtype(nameof(Opcode), Opcodes.Packet2, typeof(DerivedModel2))]
        [BFSubtypeFactory(nameof(Opcode), typeof(PacketFactory))]
        [BFSubtypeDefault(typeof(DefaultDerModel))]
        public RootModel Data { get; set; }
    }

    public class DerivedModel1 : RootModel
    {
        public string HelloWorld { get; set; }
    }

    public class DerivedModel2 : RootModel
    {
        public byte Byter { get; set; }
    }

    public class DefaultDerModel : RootModel
    {
        public byte Byterok { get; set; }
    }

    public class FactoryDerModel : RootModel
    {
        public int Kolbason { get; set; }
    }

    public class RootModel
    {
        public int Bublik { get; set; }
    }

    public enum Opcodes
    {
        Packet1,
        Packet2,
        Packet3,
        Packet4,
        Packet5
    }

    public class RecursiveModel : ByteModel<RecursiveModel>
    {
        public int Int { get; set; }
        public IPModel IP { get; set; }
    }

    public class IPModel : ByteModel<IPModel>
    {
        public IPAddress Address { get; set; }
    }
}