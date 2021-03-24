using System;
using System.Drawing;
using System.Windows.Forms;
using SuperCopyPaste.Core;
using SuperCopyPaste.Models;

namespace SuperCopyPaste
{
    public class DataGridViewCustomCell : DataGridViewTextBoxCell
    {
        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // Call the base class method to paint the default cell appearance.
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                value, formattedValue, errorText, cellStyle,
                advancedBorderStyle, paintParts);

            var cellValue = (ClipboardDataModel) value;

            if (cellValue == null) return;

            var newRect = new Rectangle(cellBounds.X + 1,
                cellBounds.Y + 1, cellBounds.Width - 4,
                cellBounds.Height - 4);


            graphics.FillRectangle(
                (cellState & DataGridViewElementStates.Selected) != 0 ? Brush(cellStyle) : Brushes.White, newRect);


            if (cellValue.ClipboardType == ClipboardType.Text)
            {
                graphics.DrawString(cellValue.Text, cellStyle.Font,
                    (cellState & DataGridViewElementStates.Selected) != 0 ? Brushes.White : Brushes.Black, newRect,
                    StringFormat.GenericDefault);
            }
            else
            {
                var myCallback =
                    new Image.GetThumbnailImageAbort(ThumbnailCallback);

                var image = (Image) cellValue.Image;
                if (image == null)
                {
                    return;
                }

                var factor = (float) image.Height / (cellBounds.Height - 2);

                var thumbnailImage = image.GetThumbnailImage((int) (image.Width / factor), cellBounds.Height - 5, myCallback,
                    IntPtr.Zero);
                graphics.DrawImage(thumbnailImage, new Point(cellBounds.X + 2, cellBounds.Y + 2));
            }
        }

        private SolidBrush _brush;

        private  SolidBrush Brush(DataGridViewCellStyle cellStyle)
        {
            return _brush ?? (_brush = new SolidBrush(cellStyle.SelectionBackColor));
        }

        private static bool ThumbnailCallback()
        {
            return false;
        }

        // Force the cell to repaint itself when the mouse pointer enters it.
        protected override void OnMouseEnter(int rowIndex)
        {
            DataGridView.InvalidateCell(this);
        }

        // Force the cell to repaint itself when the mouse pointer leaves it.
        protected override void OnMouseLeave(int rowIndex)
        {
            DataGridView.InvalidateCell(this);
        }
    }
}