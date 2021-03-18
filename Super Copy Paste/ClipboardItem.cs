using System;
using SuperCopyPaste;

namespace Super_Copy_Paste
{
    [Serializable]
    public class ClipboardItem
    {
        public DateTime Created { get; set; } = DateTime.Now;

        public Guid Id { get; set; } = Guid.NewGuid();

        public ClipboardData Data { get; set; }

        public bool Pinned { get; set; }
    }
}