using SuperCopyPaste.Controls;
using SuperCopyPaste.Core;
using SuperCopyPaste.Models;

namespace SuperCopyPaste
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Data = new SuperCopyPaste.DataGridViewRolloverCellColumn();
            this.createdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.clipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteItemsOlderThan1DayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteItemsOlderThan3DaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteItemsOlderThanOneWeekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteItemsOlderThanOneMonthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unpinAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOnlyPinnedItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentRecordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totalRecordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel = new SuperCopyPaste.Controls.PanelEx();
            this.txtBox = new SuperCopyPaste.Controls.CueTextBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.ColumnHeadersVisible = false;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Data,
            this.createdDataGridViewTextBoxColumn});
            this.dataGridView.ContextMenuStrip = this.contextMenuStrip;
            this.dataGridView.DataSource = this.bindingSource;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 31);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 30;
            this.dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.ShowRowErrors = false;
            this.dataGridView.Size = new System.Drawing.Size(979, 517);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentDoubleClick);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            // 
            // Data
            // 
            this.Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Data.DataPropertyName = "Data";
            this.Data.HeaderText = "Content";
            this.Data.MinimumWidth = 6;
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            this.Data.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // createdDataGridViewTextBoxColumn
            // 
            this.createdDataGridViewTextBoxColumn.DataPropertyName = "Created";
            this.createdDataGridViewTextBoxColumn.HeaderText = "Created";
            this.createdDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.createdDataGridViewTextBoxColumn.Name = "createdDataGridViewTextBoxColumn";
            this.createdDataGridViewTextBoxColumn.ReadOnly = true;
            this.createdDataGridViewTextBoxColumn.Width = 125;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pinToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(123, 52);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // pinToolStripMenuItem
            // 
            this.pinToolStripMenuItem.CheckOnClick = true;
            this.pinToolStripMenuItem.Name = "pinToolStripMenuItem";
            this.pinToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.pinToolStripMenuItem.Text = "Pin";
            this.pinToolStripMenuItem.Click += new System.EventHandler(this.pinToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // bindingSource
            // 
            this.bindingSource.AllowNew = true;
            this.bindingSource.DataSource = typeof(SuperCopyPaste.Models.ClipboardItemModel);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipTitle = "Super Copy Paste";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Super Copy Paste";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clipboardToolStripMenuItem,
            this.currentRecordsToolStripMenuItem,
            this.totalRecordsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(3, 3);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(979, 28);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip1";
            // 
            // clipboardToolStripMenuItem
            // 
            this.clipboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllToolStripMenuItem,
            this.deleteItemsOlderThan1DayToolStripMenuItem,
            this.deleteItemsOlderThan3DaysToolStripMenuItem,
            this.deleteItemsOlderThanOneWeekToolStripMenuItem,
            this.deleteItemsOlderThanOneMonthToolStripMenuItem,
            this.unpinAllToolStripMenuItem,
            this.showOnlyPinnedItemsToolStripMenuItem});
            this.clipboardToolStripMenuItem.Name = "clipboardToolStripMenuItem";
            this.clipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.clipboardToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.clipboardToolStripMenuItem.Text = "&Clipboard";
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(340, 26);
            this.clearAllToolStripMenuItem.Text = "Clear &all";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // deleteItemsOlderThan1DayToolStripMenuItem
            // 
            this.deleteItemsOlderThan1DayToolStripMenuItem.Name = "deleteItemsOlderThan1DayToolStripMenuItem";
            this.deleteItemsOlderThan1DayToolStripMenuItem.Size = new System.Drawing.Size(340, 26);
            this.deleteItemsOlderThan1DayToolStripMenuItem.Text = "Delete items older than &1 day";
            this.deleteItemsOlderThan1DayToolStripMenuItem.Click += new System.EventHandler(this.deleteItemsOlderThan1DayToolStripMenuItem_Click);
            // 
            // deleteItemsOlderThan3DaysToolStripMenuItem
            // 
            this.deleteItemsOlderThan3DaysToolStripMenuItem.Name = "deleteItemsOlderThan3DaysToolStripMenuItem";
            this.deleteItemsOlderThan3DaysToolStripMenuItem.Size = new System.Drawing.Size(340, 26);
            this.deleteItemsOlderThan3DaysToolStripMenuItem.Text = "Delete items older than &3 days";
            this.deleteItemsOlderThan3DaysToolStripMenuItem.Click += new System.EventHandler(this.deleteItemsOlderThan3DaysToolStripMenuItem_Click);
            // 
            // deleteItemsOlderThanOneWeekToolStripMenuItem
            // 
            this.deleteItemsOlderThanOneWeekToolStripMenuItem.Name = "deleteItemsOlderThanOneWeekToolStripMenuItem";
            this.deleteItemsOlderThanOneWeekToolStripMenuItem.Size = new System.Drawing.Size(340, 26);
            this.deleteItemsOlderThanOneWeekToolStripMenuItem.Text = "Delete items older than one &week";
            this.deleteItemsOlderThanOneWeekToolStripMenuItem.Click += new System.EventHandler(this.deleteItemsOlderThanOneWeekToolStripMenuItem_Click);
            // 
            // deleteItemsOlderThanOneMonthToolStripMenuItem
            // 
            this.deleteItemsOlderThanOneMonthToolStripMenuItem.Name = "deleteItemsOlderThanOneMonthToolStripMenuItem";
            this.deleteItemsOlderThanOneMonthToolStripMenuItem.Size = new System.Drawing.Size(340, 26);
            this.deleteItemsOlderThanOneMonthToolStripMenuItem.Text = "Delete items older than one &month";
            this.deleteItemsOlderThanOneMonthToolStripMenuItem.Click += new System.EventHandler(this.deleteItemsOlderThanOneMonthToolStripMenuItem_Click);
            // 
            // unpinAllToolStripMenuItem
            // 
            this.unpinAllToolStripMenuItem.Name = "unpinAllToolStripMenuItem";
            this.unpinAllToolStripMenuItem.Size = new System.Drawing.Size(340, 26);
            this.unpinAllToolStripMenuItem.Text = "&Unpin all";
            this.unpinAllToolStripMenuItem.Click += new System.EventHandler(this.unpinAllToolStripMenuItem_Click);
            // 
            // showOnlyPinnedItemsToolStripMenuItem
            // 
            this.showOnlyPinnedItemsToolStripMenuItem.CheckOnClick = true;
            this.showOnlyPinnedItemsToolStripMenuItem.Name = "showOnlyPinnedItemsToolStripMenuItem";
            this.showOnlyPinnedItemsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.showOnlyPinnedItemsToolStripMenuItem.Size = new System.Drawing.Size(340, 26);
            this.showOnlyPinnedItemsToolStripMenuItem.Text = "Show only pinned items";
            this.showOnlyPinnedItemsToolStripMenuItem.Click += new System.EventHandler(this.showOnlyPinnedItemsToolStripMenuItem_Click);
            // 
            // currentRecordsToolStripMenuItem
            // 
            this.currentRecordsToolStripMenuItem.Name = "currentRecordsToolStripMenuItem";
            this.currentRecordsToolStripMenuItem.Size = new System.Drawing.Size(157, 24);
            this.currentRecordsToolStripMenuItem.Text = "Displayed records: 0";
            // 
            // totalRecordsToolStripMenuItem
            // 
            this.totalRecordsToolStripMenuItem.Name = "totalRecordsToolStripMenuItem";
            this.totalRecordsToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
            this.totalRecordsToolStripMenuItem.Text = "Total Records";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.Gray;
            this.panel.Controls.Add(this.dataGridView);
            this.panel.Controls.Add(this.menuStrip);
            this.panel.Controls.Add(this.txtBox);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Padding = new System.Windows.Forms.Padding(3);
            this.panel.Size = new System.Drawing.Size(985, 573);
            this.panel.TabIndex = 3;
            // 
            // txtBox
            // 
            this.txtBox.Cue = "Start typing to filter records";
            this.txtBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtBox.Location = new System.Drawing.Point(3, 548);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(979, 22);
            this.txtBox.TabIndex = 1;
            this.txtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Created";
            this.dataGridViewTextBoxColumn1.HeaderText = "Created";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 573);
            this.Controls.Add(this.panel);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Super Copy Paste";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private DataGridViewRolloverCellColumn itemDataDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private CueTextBox txtBox;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem clipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteItemsOlderThan1DayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteItemsOlderThan3DaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteItemsOlderThanOneWeekToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteItemsOlderThanOneMonthToolStripMenuItem;
        private DataGridViewRolloverCellColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem currentRecordsToolStripMenuItem;
        private PanelEx panel;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem pinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unpinAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOnlyPinnedItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totalRecordsToolStripMenuItem;
    }
}

