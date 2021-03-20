using System;
using System.Windows.Forms;

namespace SuperCopyPaste.Core
{
    // Must inherit Control, not Component, in order to have Handle
    public class ClipboardChangedEventArgs : EventArgs
    {
        public ClipboardChangedEventArgs(IDataObject dataObject)
        {
            DataObject = dataObject;
        }

        public IDataObject DataObject { get; }
    }
}