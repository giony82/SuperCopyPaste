using System.Windows.Forms;

namespace SuperCopyPaste.Controls
{
    public class PanelEx : Panel
    {
        #region Constructors and Destructors

        public PanelEx()
        {
            // ReSharper disable once RedundantBaseQualifier
            base.DoubleBuffered = true;
            UpdateStyles();
        }

        #endregion


        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED

                return cp;
            }
        }

        #region Public Properties

        #endregion

        #region Fields

        #endregion
    }
}