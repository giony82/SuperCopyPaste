using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SuperCopyPaste.Controls
{
    internal class CueTextBox : TextBox
    {
        private string _cue;

        [Localizable(true)]
        public string Cue
        {
            get => _cue;
            set
            {
                _cue = value;
                UpdateCue();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateCue();
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, string lp);

        private void UpdateCue()
        {
            if (IsHandleCreated && _cue != null) SendMessage(Handle, 0x1501, (IntPtr)1, _cue);
        }
    }
}