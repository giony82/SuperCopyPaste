// ------------------------------------------------------------------------------
//     <copyright file="Config.cs" company="BlackLine">
//         Copyright (C) BlackLine. All rights reserved.
//     </copyright>
// ------------------------------------------------------------------------------

using System.Windows.Forms;

using SuperCopyPaste.Keyboard;

namespace SuperCopyPaste.Constants
{
    /// <summary>Hardcoded configurations as the combinations of keys to activate the window.</summary>
    public static class Config
    {
        // - The activate keys combination - Default to "ALT - F1"
        public static Keys ActivateKey = Keys.F1;

        public static ModifierKeys ActivateModifierKey = ModifierKeys.Alt;

        // - End activation keys.

        /// <summary>
        /// The app for which requires two ctrl v. 
        /// </summary>
        public static string DoublePasteApp = "Microsoft Visual Studio";
    }
}