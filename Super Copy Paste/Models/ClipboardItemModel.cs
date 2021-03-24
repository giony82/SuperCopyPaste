using System;

namespace SuperCopyPaste.Models
{
    [Serializable]
    public class ClipboardItemModel
    {
        public DateTime Created { get; set; } = DateTime.Now;

        public Guid Id { get; set; } = Guid.NewGuid();

        public ClipboardDataModel Data { get; set; }

        public bool Pinned { get; set; }
    }
}