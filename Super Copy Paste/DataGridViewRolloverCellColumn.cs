using System.Windows.Forms;

namespace SuperCopyPaste
{
    public class DataGridViewRolloverCellColumn : DataGridViewColumn
    {
        public DataGridViewRolloverCellColumn()
        {
            CellTemplate = new DataGridViewCustomCell();
        }
    }
}