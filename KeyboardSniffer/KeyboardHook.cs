using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace KeyboardSniffer
{
    public class KeyboardHook : IDisposable
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        private static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

        // For loading specific keyboard layout
        //[DllImport("user32.dll")]
        //private static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint Flags);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x100;
        //private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYDOWN = 0x104;
        //private const int WM_SYSKEYUP = 0x105;

        private const uint MAPVK_VK_TO_VSC = 0x00;
        //private const uint MAPVK_VSC_TO_VK = 0x01;
        //private const uint MAPVK_VK_TO_CHAR = 0x02;
        //private const uint MAPVK_VSC_TO_VK_EX = 0x03;
        //private const uint MAPVK_VK_TO_VSC_EX = 0x04;

        private IntPtr _hookID = IntPtr.Zero;
        // field to store the callback to avoid the delegate being collected by GC
        private readonly LowLevelKeyboardProc _proc;

        public event KeyboardHookEventHandler KeyboardEvent;

        /// <summary>
        /// Keyboard hook is active
        /// </summary>
        public bool IsOpen
        {
            get { return _hookID != IntPtr.Zero; }
        }

        public KeyboardHook()
        {
            _proc = HookCallback;
        }

        /// <summary>
        /// Release keyboard hook
        /// </summary>
        public void Close()
        {
            if (IsOpen)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Activate keyboard hook
        /// </summary>
        public void Open()
        {
            if (!IsOpen)
            {
                _hookID = SetHook(_proc);
            }
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }
       
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                KBDLLHOOKSTRUCT kbd = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                string s = KeyCodeToUnicode(kbd.vkCode);
                KeyboardHookEventArgs ea = new KeyboardHookEventArgs()
                {
                    KeyCode = (Keys)kbd.vkCode,
                    KeyChar = String.IsNullOrEmpty(s) ? '\0' : s[0],
                    ScanCode = kbd.scanCode,
                    Flags = kbd.flags,
                    Time = kbd.time,
                    Cancel = false
                };
                KeyboardEvent?.Invoke(this, ea);
                if (ea.Cancel)
                    return (IntPtr)1; // Cancel next hook
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        /// <summary>
        /// Converts key code to unicode string
        /// </summary>
        /// <param name="key">Key code</param>
        /// <returns>string with length 1 for letters/numbers/etc. or empty string for controls</returns>
        private static string KeyCodeToUnicode(int key)
        {
            byte[] keyboardState = new byte[255];
            bool keyboardStateStatus = GetKeyboardState(keyboardState);

            if (!keyboardStateStatus)
            {
                return String.Empty;
            }

            uint virtualKeyCode = (uint)key;
            Debug.WriteLine(virtualKeyCode, "VirtualKeyCode");
            uint scanCode = MapVirtualKey(virtualKeyCode, MAPVK_VK_TO_VSC);
            //IntPtr inputLocaleIdentifier = LoadKeyboardLayout("00000409", 1); // Load US language and keyboard
            IntPtr inputLocaleIdentifier = GetKeyboardLayout(0); // Get default keyboard (by OS setting)

            Debug.WriteLine(scanCode, "ScanCode");

            StringBuilder result = new StringBuilder(10);
            ToUnicodeEx(virtualKeyCode, scanCode, keyboardState, result, (int)5, (uint)0, inputLocaleIdentifier);

            Debug.WriteLine(result, "Result");
            return result.ToString();
        }

        #region IDisposable Support
        /// <summary>
        /// Dispose keyboard hook - release hook
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            // Free unmanaged resources (unmanaged objects) and override a finalizer below.
            if (_hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
            }
        }

        // Override a finalizer, Dispose(bool disposing) above has code to free unmanaged resources.
        ~KeyboardHook()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // Finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
