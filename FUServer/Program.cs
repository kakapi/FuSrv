using System;
using System.Collections.Generic;

using System.Windows.Forms;
using System.IO;
namespace FUServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            if (GlobalVariables.GetRegistStatus())
            {
                Application.Run(new FrmMain());
            }
            else
            {
                Application.Run(new FrmRegister());
            }


           
        }
      
    }
}
