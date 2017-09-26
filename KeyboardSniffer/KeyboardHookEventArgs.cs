using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace KeyboardSniffer
{

    public delegate void KeyboardHookEventHandler(object sender, KeyboardHookEventArgs e);

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class KeyboardHookEventArgs : System.ComponentModel.CancelEventArgs
    {
        /// <summary>
        /// Virtual key code.
        /// </summary>
        public Keys KeyCode { get; set; }
        /// <summary>
        /// Key char
        /// </summary>
        public char KeyChar { get; set; }
        /// <summary>
        /// A hardware scan code for the key.
        /// </summary>
        public int ScanCode { get; set; }
        /// <summary>
        /// The extended-key flag, event-injected flags, context code, and transition-state flag. 
        /// </summary>
        public int Flags { get; set; }
        /// <summary>
        /// The message time is a long integer that specifies the elapsed time, in milliseconds, 
        /// from the time the system was started to the time the message was created 
        /// (that is, placed in the thread's message queue).
        /// </summary>
        public int Time { get; set; }

        private string DebuggerDisplay
        {
            get { return $"Key code: {KeyCode}, scan code: {ScanCode}, char: 0x{Convert.ToInt32(KeyCode):X2}"; }
        }
    }
}
