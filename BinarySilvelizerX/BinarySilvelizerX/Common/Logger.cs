using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BinarySilvelizerX.Common
{
    public static class Logger
    {
        public static MessageType Mode { get; set; } = MessageType.Warning;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Write(string message, string module, MessageType msgType)
        {
            if (Mode < msgType) return;
            Debug.WriteLine($"[{DateTime.Now}][BinarySilvelizer][{msgType}][{module}] {message}");
        }

        public enum MessageType
        {
            Error,
            Warning,
            Info
        }
    }
}