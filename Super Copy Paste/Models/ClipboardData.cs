﻿using System;
using SuperCopyPaste.Core;

namespace SuperCopyPaste.Models
{
    [Serializable]
    public class ClipboardData
    {
        public string Text { get; set; }

        public object Image { get; set; }

        public ClipboardType ClipboardType => Image != null ? ClipboardType.Image : ClipboardType.Text;
    }
}