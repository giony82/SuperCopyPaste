using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using SuperCopyPaste.Controls;
using SuperCopyPaste.Core;
using SuperCopyPaste.Keyboard;
using SuperCopyPaste.Models;
using SuperCopyPaste.Properties;

namespace SuperCopyPaste
{
    public partial class MainForm : Form
    {
        private readonly ClipboardDataManagement _clipboardDataManagement;

        private readonly KeyboardHook _keyboardHook = new KeyboardHook();

        private ClipboardMonitor _clipboardMonitor;

        private IntPtr _currentFocusedWindow;

        private bool _suppressClipboardMonitoring;

        private const int ImageHeight = 150;

        private const int TextHeight = 35;

        public MainForm()
        {
            InitializeComponent();

            _clipboardDataManagement = new ClipboardDataManagement(new FileClipboardStorage());
            _clipboardDataManagement.CountChanged += ClipboardDataManagementCountChanged;
            _clipboardDataManagement.Load();

            InitClipboardMonitor();

            dataGridView.DataSource = _clipboardDataManagement.DataSource;

            _keyboardHook.KeyPressed += hook_KeyPressed;
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

            _keyboardHook.RegisterHotKey(Keyboard.ModifierKeys.Control, Keys.Enter);

            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private ClipboardItem CurrentClipboardItem
        {
            get
            {
                if (dataGridView.SelectedRows.Count == 1)
                {
                    return (ClipboardItem) dataGridView.SelectedRows[0].DataBoundItem;
                }

                return null;
            }
        }

        // The GetForegroundWindow function returns a handle to the foreground window
        // (the window  with which the user is currently working).
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveClipboardItems();

            base.OnClosing(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            ResizeRows();
            base.OnLoad(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if (msg.Msg == WM_KEYDOWN || msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Escape:
                        SendToTray();
                        return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _clipboardDataManagement.Clear();
        }

        private void ClipboardDataManagementCountChanged(object sender, int count)
        {
            currentRecordsToolStripMenuItem.Text = $"Current records:{count}";
        }

        private void ClipboardMonitorClipboardChanged(object sender, ClipboardChangedEventArgs e)
        {
            if (_suppressClipboardMonitoring)
            {
                return;
            }

            IDataObject d = Clipboard.GetDataObject();

            _clipboardDataManagement.AddClipboardData(d);

            dataGridView.Sort(createdDataGridViewTextBoxColumn, ListSortDirection.Descending);

            BeginInvoke(new MethodInvoker(ResizeRows));
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Paste();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentClipboardItem != null)
            {
                Paste();
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Delete && CurrentClipboardItem != null)
            {
                _clipboardDataManagement.Delete(CurrentClipboardItem);
                e.SuppressKeyPress = true;
            }
            else if (char.IsLetterOrDigit((char) e.KeyCode) || e.KeyCode == Keys.Back)
            {
                txtBox.Focus();
                SendKeys.Send(((char) e.KeyCode).ToString());
            }
        }

        private void deleteItemsOlderThan1DayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _clipboardDataManagement.DeleteItems(1);
        }

        private void deleteItemsOlderThan3DaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _clipboardDataManagement.DeleteItems(3);
        }

        private void deleteItemsOlderThanOneMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _clipboardDataManagement.DeleteItems(30);
        }

        private void deleteItemsOlderThanOneWeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _clipboardDataManagement.DeleteItems(7);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.Are_you_sure_you_want_to_close_the_application, Resources.Super_Copy_Paste, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _clipboardDataManagement.Save();
                Application.Exit();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            _currentFocusedWindow = GetForegroundWindow();
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
            dataGridView.Focus();
            Activate();
        }

        private void InitClipboardMonitor()
        {
            _clipboardMonitor = new ClipboardMonitor();
            Controls.Add(_clipboardMonitor);
            _clipboardMonitor.ClipboardChanged += ClipboardMonitorClipboardChanged;
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void Paste()
        {
            if (CurrentClipboardItem == null)
            {
                return;
            }

            _suppressClipboardMonitoring = true;
            try
            {
                switch (CurrentClipboardItem.Data.ClipboardType)
                {
                    case ClipboardType.Text:
                        Clipboard.SetText(CurrentClipboardItem.Data.Text);
                        break;
                    case ClipboardType.Image:
                        Clipboard.SetImage((Image) CurrentClipboardItem.Data.Image);
                        break;
                }
            }
            finally
            {
                _suppressClipboardMonitoring = false;
            }

            SendToTray();

            SetForegroundWindow(_currentFocusedWindow);

            SendKeys.Send("^v");
        }

        private void ResizeRows()
        {
            foreach (DataGridViewRow dataGridViewRow in dataGridView.Rows)
            {
                if (dataGridViewRow.DataBoundItem is ClipboardItem c)
                {
                    var isImage = c.Data.ClipboardType == ClipboardType.Image;
                    var height = isImage ? ImageHeight : TextHeight;
                    dataGridViewRow.Height = height;
                }
            }
        }

        private void SaveClipboardItems()
        {
            try
            {
                _clipboardDataManagement.Save();
            }
            catch (Exception err)
            {
                MessageBox.Show(string.Format(Resources.Cant_save_clipboard_data_0, err.Message),
                    Resources.Super_Copy_Paste, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendToTray()
        {
            Hide();
            notifyIcon.Visible = true;
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    _clipboardMonitor.ClipboardChanged += ClipboardMonitorClipboardChanged;
                    break;
            }
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView.Focus();
                Paste();
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Up)
            {
                dataGridView.Focus();
                BeginInvoke(
                    new MethodInvoker(
                        delegate { SendKeys.Send("{UP}"); }));
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Down)
            {
                dataGridView.Focus();
                BeginInvoke(
                    new MethodInvoker(
                        delegate { SendKeys.Send("{DOWN}"); }));
                e.Handled = true;
            }
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            var filterCriteria = new FilterCriteria {
                Text = txtBox.Text, 
                Pinned = showOnlyPinnedItemsToolStripMenuItem.Checked
            };

            _clipboardDataManagement.Filter(filterCriteria);

            ResizeRows();
        }

        private void pinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentClipboardItem.Pinned = pinToolStripMenuItem.Checked;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            pinToolStripMenuItem.Checked = CurrentClipboardItem.Pinned;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _clipboardDataManagement.Delete(CurrentClipboardItem);
        }

        private void unpinAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _clipboardDataManagement.UnpinAll();
        }

        private void showOnlyPinnedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filterCriteria = new FilterCriteria
            {
                Text = txtBox.Text,
                Pinned = showOnlyPinnedItemsToolStripMenuItem.Checked
            };

            _clipboardDataManagement.Filter(filterCriteria);
        }
    }
}