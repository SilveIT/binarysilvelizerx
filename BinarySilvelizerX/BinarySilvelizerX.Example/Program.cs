﻿using System;

namespace BinarySilvelizerX.Example
{
    internal class Program
    {
        private static void Main()
        {
            //Input derived models
            var inlinedModel1 = new DerivedModel1 { Bublik = 1, HelloWorld = "hello" };
            var inlinedModel2 = new DerivedModel2 { Bublik = 2, Byter = 0xFF };
            var inlinedModel3 = new DefaultDerModel { Bublik = 3, Byterok = 0xFE };
            var inlinedModel4 = new FactoryDerModel { Bublik = 4, Kolbason = 1337 };
            var inlinedModel5 = new FactoryDerModel { Bublik = 5, Kolbason = 22848 };

            //Input models filled with data as root models
            var model1 = new UniversalPacket { Opcode = Opcodes.Packet1, Data = inlinedModel1 };
            var model2 = new UniversalPacket { Opcode = Opcodes.Packet2, Data = inlinedModel2 };
            var model3 = new UniversalPacket { Opcode = Opcodes.Packet5, Data = inlinedModel3 };
            var model4 = new UniversalPacket { Opcode = Opcodes.Packet3, Data = inlinedModel4 };
            var model5 = new UniversalPacket { Opcode = Opcodes.Packet4, Data = inlinedModel5 };

            //Serialized data
            byte[] bytes1 = model1;
            byte[] bytes2 = model2;
            byte[] bytes3 = model3;
            byte[] bytes4 = model4;
            byte[] bytes5 = model5;

            //Deserialized models
            var outPacket1 = (UniversalPacket)bytes1;
            var outPacket2 = (UniversalPacket)bytes2;
            var outPacket3 = (UniversalPacket)bytes3;
            var outPacket4 = (UniversalPacket)bytes4;
            var outPacket5 = (UniversalPacket)bytes5;

            //Checking what we've got
            Console.WriteLine($"Opcode: {outPacket1.Opcode}, " +
                              $"bublik: {outPacket1.Data.Bublik}, " +
                              $"HelloWorld: {((DerivedModel1)outPacket1.Data).HelloWorld}");
            Console.WriteLine($"Opcode: {outPacket2.Opcode}, " +
                              $"bublik: {outPacket2.Data.Bublik}, " +
                              $"byter: {((DerivedModel2)outPacket2.Data).Byter}");
            Console.WriteLine($"Opcode: {outPacket3.Opcode}, " +
                              $"bublik: {outPacket3.Data.Bublik}, " +
                              $"byterok: {((DefaultDerModel)outPacket3.Data).Byterok}");
            Console.WriteLine($"Opcode: {outPacket3.Opcode}, " +
                              $"bublik: {outPacket3.Data.Bublik}, " +
                              $"kolbason: {((FactoryDerModel)outPacket4.Data).Kolbason}");
            Console.WriteLine($"Opcode: {outPacket3.Opcode}, " +
                              $"bublik: {outPacket3.Data.Bublik}, " +
                              $"kolbason: {((FactoryDerModel)outPacket5.Data).Kolbason}");
            Console.ReadKey();
        }
    }
}