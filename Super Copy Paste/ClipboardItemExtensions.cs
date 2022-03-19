using System;
using System.Drawing;
using System.Windows.Forms;
using SuperCopyPaste.Constants;
using SuperCopyPaste.Core;
using SuperCopyPaste.Models;

namespace SuperCopyPaste
{
    public static class ClipboardItemExtensions
    {
        public static void CopyToClipboard(this ClipboardItemModel clipboardItem)
        {
            switch (clipboardItem.Data.ClipboardType)
            {
                case ClipboardType.Text:
                    Clipboard.SetText(clipboardItem.Data.Text);
                    break;
                case ClipboardType.Image:
                    Clipboard.SetImage((Image)clipboardItem.Data.Image);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static int GetHeight(this ClipboardItemModel clipboardItem)
        {
            bool isImage = clipboardItem.Data.ClipboardType == ClipboardType.Image;
            int height = isImage ? UIConstants.ImageRowHeight : UIConstants.TextRowHeight;
            return height;
        }
    }
}