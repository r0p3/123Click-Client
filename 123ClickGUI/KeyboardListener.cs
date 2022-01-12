using Gma.System.MouseKeyHook;
using System;
using System.Windows.Forms;

namespace _123ClickGUI
{
    class KeyboardListener
    {
        private IKeyboardEvents keyboardEvents;

        private Keys first = Keys.None;
        private Keys second = Keys.None;

        private Keys modifier = Keys.CapsLock;

        public event EventHandler onCountDownToggle;
        public event EventHandler onRewind;
        public event EventHandler onForward;
        public KeyboardListener()
        {
            keyboardEvents = Hook.GlobalEvents();
            keyboardEvents.KeyDown += KeyboardEvents_KeyDown;
            keyboardEvents.KeyUp += KeyboardEvents_KeyUp;
        }

        private void KeyboardEvents_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.RControlKey)
            {
                if (e.KeyCode == first)
                {
                    first = Keys.None;
                }
                if (e.KeyCode == second)
                {
                    second = Keys.None;
                }
            }
        }

        private void KeyboardEvents_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.LControlKey)
            {
                if (first == Keys.None)
                {
                    first = e.KeyCode;
                }
                else if (second == Keys.None && first != e.KeyCode)
                {
                    second = e.KeyCode;
                }
                if (first != Keys.None && second != Keys.None)
                    hotkeyCheck();
            }
        }

        private void hotkeyCheck()
        {
            if (first == modifier || second == modifier)
            {
                switch ((first != modifier) ? first : second)
                {
                    case Keys.S:
                        onCountDownToggle?.Invoke(this, null);
                        break;
                    case Keys.R:
                        onRewind?.Invoke(this, null);
                        break;
                    case Keys.F:
                        onForward?.Invoke(this, null);
                        break;
                    default:
                        break;
                }
            }
            first = Keys.None;
            second = Keys.None;
        }
    }
}
