using System;
using Super_Copy_Paste;

namespace SuperCopyPaste
{
    [Serializable]
    public class ClipboardData
    {
        public string Text { get; set; }

        public object Image { get; set; }

        public ClipboardType ClipboardType { get; set; }
    }
}