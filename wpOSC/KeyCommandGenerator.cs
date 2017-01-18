using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace wpOSC
{
    internal enum KeyState
    {
        PRESS = 0,
        RELEASE = 1
    }

    internal struct KeyCommand
    {
        internal KeyState keyState;
        internal int keyCode;
        internal string s;

        internal KeyCommand(KeyState keyState, int keyCode, string s)
        {
            this.keyState = keyState;
            this.keyCode = keyCode;
            this.s = s;
        }
    }

    class KeyCommandGenerator
    {
        /// <summary>
        /// The keys which need the shift to be pressed when typed.
        /// </summary>
        private static String SHIFT_KEYS = "_\"~:+|?!@#$%^&*<>(){}";

        private static int SHIFT_CODE = 59;

        private static Dictionary<char, int> KEY_CODES = new Dictionary<char, int>(40);

        // The mapping of the control key codes from the enum Key -> the key code to send.
        internal static Dictionary<Key, int> CONTROL_KEY_CODES = new Dictionary<Key, int>();

        static KeyCommandGenerator()
        {
            KEY_CODES.Add('~', 68);
            KEY_CODES.Add('`', 68);
            KEY_CODES.Add('_', 69);
            KEY_CODES.Add('-', 69);
            KEY_CODES.Add('=', 70);
            KEY_CODES.Add('+', 70);
            KEY_CODES.Add('{', 71);
            KEY_CODES.Add('}', 72);
            KEY_CODES.Add('[', 71);
            KEY_CODES.Add(']', 72);
            KEY_CODES.Add('\\', 73);
            KEY_CODES.Add('|', 73);
            KEY_CODES.Add(':', 74);
            KEY_CODES.Add(';', 74);
            KEY_CODES.Add('\'', 75);
            KEY_CODES.Add('"', 75);
            KEY_CODES.Add('?', 76);
            KEY_CODES.Add('/', 76);
            KEY_CODES.Add('!', 8);
            KEY_CODES.Add('@', 77);
            KEY_CODES.Add('#', 10);
            KEY_CODES.Add('$', 11);
            KEY_CODES.Add('%', 12);
            // KEY_CODES.Add('^', 13);
            KEY_CODES.Add('&', 14);
            KEY_CODES.Add('*', 15);
            // KEY_CODES.Add('\u8226', 15); 
            KEY_CODES.Add('(', 16);
            KEY_CODES.Add(')', 7);

            KEY_CODES.Add('<', 55);
            KEY_CODES.Add('>', 56);

            KEY_CODES.Add(',', 55);
            KEY_CODES.Add('.', 56);

            KEY_CODES.Add(' ', 62);

            CONTROL_KEY_CODES.Add(Key.Back, 67);
            CONTROL_KEY_CODES.Add(Key.Enter, 66);
        }

        // TODO: this should probably take a string instead
        internal IList<KeyCommand> generateCommands(char c)
        {
            IList<KeyCommand> commands = new List<KeyCommand>();
            bool shiftNeeded = Char.IsUpper(c) || SHIFT_KEYS.IndexOf(c) > -1;

            // translate the codes to ASCII
            if (Char.IsDigit(c))
            {
                int keyCode = c == '0' ? 8 : ((int)c) - 41;
                commands.Add(new KeyCommand(KeyState.PRESS, keyCode, new String(c, 1))); 
                commands.Add(new KeyCommand(KeyState.RELEASE, keyCode, new String(c, 1))); 
            }
            else if (Char.IsLetter(c))
            {
                // a on server is 29, key codes on phone are ASCII (A = 65, a = 97)
                int keyCode = (int)c - (Char.IsUpper(c) ? 36 : 68);
                commands.Add(new KeyCommand(KeyState.PRESS, keyCode, new String(c, 1))); 
                commands.Add(new KeyCommand(KeyState.RELEASE, keyCode, new String(c, 1))); 
            }
            else if (KEY_CODES.ContainsKey(c))
            {
                commands.Add(new KeyCommand(KeyState.PRESS, KEY_CODES[c], new String(c, 1)));
                commands.Add(new KeyCommand(KeyState.RELEASE, KEY_CODES[c], new String(c, 1)));
            }
            else
            {
                System.Windows.MessageBox.Show("not mapped " + c + " " + ((int)c));
                // TODO: debug
            }

            if (shiftNeeded)
            {
                commands.Insert(0, new KeyCommand(KeyState.PRESS, SHIFT_CODE, "\0"));
                commands.Add(new KeyCommand(KeyState.RELEASE, SHIFT_CODE, "\0"));
            }

            return commands;
        }
    }
}
