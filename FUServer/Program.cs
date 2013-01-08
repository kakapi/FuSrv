using System;
using System.Collections.Generic;

using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
namespace FUServer
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            FrmMain frmMain = null;
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (!GlobalVariables.GetRegistStatus())
                {
                    FrmRegister fr = new FrmRegister();
                    if (fr.ShowDialog() == DialogResult.OK)
                    {
                        frmMain = new FrmMain();
                        Application.Run(frmMain);
                    }
                }
                else
                {
                    frmMain = new FrmMain();
                    Application.Run(frmMain);
                }
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("服务已经启动.");

                if (frmMain != null)
                {
                    frmMain.BringToFront();
                }
            }
           

        }
    }
    internal class NativeMethods
    {
        public const int HWND_BROADCAST = 0xffff;
        public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);
    }
}
