using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeyboardSniffer
{
    public partial class FormMain : Form
    {
        private readonly KeyboardHook kbdHook;

        public FormMain()
        {
            InitializeComponent();
            kbdHook = new KeyboardHook();
            kbdHook.KeyboardEvent += KbdHook_KeyboardEvent;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            kbdHook.Open();
        }

        private void KbdHook_KeyboardEvent(object sender, KeyboardHookEventArgs e)
        {
            char ch = e.KeyChar;
            int charCode = Convert.ToInt32(ch);
            UnicodeCategory charCategory = Char.GetUnicodeCategory(ch);
            if (charCategory == UnicodeCategory.Control)
                ch = ' ';
            textBoxKeyLog.Text += $"Key code: {e.KeyCode}, scan code: {e.ScanCode}, char: '{ch}' 0x{charCode:X2} \r\n";
            textBoxKeyLog.SelectionStart = textBoxKeyLog.TextLength;
            textBoxKeyLog.ScrollToCaret();
        }

        private void toolStripButtonClear_Click(object sender, EventArgs e)
        {
            textBoxKeyLog.Text = String.Empty;
            textBoxKeyLog.SelectionStart = textBoxKeyLog.TextLength;
            textBoxKeyLog.ScrollToCaret();
        }

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxKeyLog.Text))
                Clipboard.SetText(textBoxKeyLog.Text, TextDataFormat.UnicodeText);
        }

        private void toolStripButtonInfo_Click(object sender, EventArgs e)
        {
            using (Form dlg = new DialogAbout())
            {
                dlg.ShowDialog(this);
            }
        }

        private void toolStripButtonTopMost_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            toolStripButtonTopMost.Checked = TopMost;
            toolStripButtonTopMost.Image = TopMost ? Properties.Resources.IconLockOpen : Properties.Resources.IconLock;
        }
    }
}
