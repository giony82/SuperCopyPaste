﻿using System;
using System.Windows.Forms;

namespace SuperCopyPaste.Keyboard
{
    /// <summary>Event Args for the event that is fired after the hot key has been pressed.</summary>
    public class KeyPressedEventArgs : EventArgs
    {
        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            Modifier = modifier;
            Key = key;
        }

        public Keys Key { get; }

        public ModifierKeys Modifier { get; }
    }
}