using System;
using System.Drawing;
using SuperCopyPaste.Core;

namespace SuperCopyPaste.Models
{
    [Serializable]
    public class ClipboardDataModel
    {
        public string Text { get; set; }

        public Bitmap Image { get; set; }

        public ClipboardType ClipboardType => Image != null ? ClipboardType.Image : ClipboardType.Text;
    }
}