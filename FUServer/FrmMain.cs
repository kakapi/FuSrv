using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using FUServer.Properties;
using Socktes;
namespace FUServer
{
    public partial class FrmMain : Form
    {
       
        bool started = true;
        public FrmMain()
        {
            InitializeComponent();
            SetAutoRun(true);
            StartService();
        }
        private void SetButtonStatus()
        {
            btnStart.Enabled = !started;
            btnStop.Enabled = started;
            if (started)
            {
                btnStart.Text = "已启动";
                btnStop.Text = "停止";
            }
            else
            {
                btnStart.Text = "启动";
                btnStop.Text = "已停止";
            }
        }
        private void Log(string message)
        {
            GlobalVariables.Logger.Info(message);
            tbxLog.AppendText(DateTime.Now + "  " + message + Environment.NewLine);
        }

        private void StartService()
        {
            new SocketAction().StartServer(msgHandler);
            started = true;
            SetButtonStatus();
            Log("服务启动");

            
        }
        void msgHandler(string msg)
        {
            Log(msg);
        }

    

        private void btnStop_Click(object sender, EventArgs e)
        {
            started = false;
         

            SetButtonStatus();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartService();

        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            new FuLib.FuSocket().StopServer();
            
            Application.Exit();

        }

        private void SetAutoRun(bool enable)
        {
            string appName = "FuService";
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (enable)
                rk.SetValue(appName, Application.ExecutablePath.ToString());
            else
                rk.DeleteValue(appName, false);

        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            new CryWin().Show();
        }
    }
}
