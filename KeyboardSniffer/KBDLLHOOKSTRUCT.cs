using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace KeyboardSniffer
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct KBDLLHOOKSTRUCT
    {
        public Int32 vkCode;
        public Int32 scanCode;
        public Int32 flags;
        public Int32 time;
        public IntPtr dwExtraInfo;
    }
}
