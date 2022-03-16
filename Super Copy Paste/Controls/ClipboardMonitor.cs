using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SuperCopyPaste.Core;

namespace SuperCopyPaste.Controls
{
    [DefaultEvent("ClipboardChanged")]
    public class ClipboardMonitor : Control
    {
        private IntPtr _nextClipboardViewer;

        public ClipboardMonitor()
        {
            Visible = false;

            _nextClipboardViewer = (IntPtr)SetClipboardViewer((int)Handle);
        }

        /// <summary>Clipboard contents changed.</summary>
        public event EventHandler<ClipboardChangedEventArgs> ClipboardChanged;

        protected override void Dispose(bool disposing)
        {
            ChangeClipboardChain(Handle, _nextClipboardViewer);
        }

        protected override void WndProc(ref Message m)
        {
            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    OnClipboardChanged();
                    SendMessage(_nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == _nextClipboardViewer)
                        _nextClipboardViewer = m.LParam;
                    else
                        SendMessage(_nextClipboardViewer, m.Msg, m.WParam, m.LParam);

                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        private static extern int SetClipboardViewer(int hWndNewViewer);

        private void OnClipboardChanged()
        {
            try
            {
                IDataObject iData = Clipboard.GetDataObject();
                ClipboardChanged?.Invoke(this, new ClipboardChangedEventArgs(iData));
            }
            catch (Exception e)
            {
                Trace.Write(e.ToString());
            }
        }
    }
}