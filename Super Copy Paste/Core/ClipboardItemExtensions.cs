using System.Windows;
using System.Windows.Media.Imaging;
using SuperCopyPaste.Constants;
using SuperCopyPaste.Models;

namespace SuperCopyPaste.Core
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
                    Clipboard.SetImage((BitmapSource) clipboardItem.Data.Image);
                    break;
            }
        }

        public static int GetHeight(this ClipboardItemModel clipboardItem)
        {
            var isImage = clipboardItem.Data.ClipboardType == ClipboardType.Image;
            var height = isImage ? UIConstants.ImageRowHeight : UIConstants.TextRowHeight;
            return height;
        }
    }
}