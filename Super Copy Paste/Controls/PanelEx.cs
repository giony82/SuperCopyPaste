using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperCopyPaste.Controls
{
    public class PanelEx : Panel
    {
        #region Public Properties

        #endregion


        protected override CreateParams CreateParams
        {
            get {
                var cp = base.CreateParams;
                if (Useoptimiziaton)
                {
                    cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                }

                return cp;
            }
        }

        #region Fields

        #endregion

        #region Constructors and Destructors

        public PanelEx()
        {
            // ReSharper disable once RedundantBaseQualifier
            base.DoubleBuffered = false;
            UpdateStyles();
        }

        public bool Useoptimiziaton { get; set; } = true;

        #endregion
    }
}
