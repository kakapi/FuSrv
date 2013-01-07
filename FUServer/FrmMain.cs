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
namespace FUServer
{
    public partial class FrmMain : Form
    {
       
        bool started = true;
        public FrmMain()
        {
            InitializeComponent();
          //  SetAutoRun(true);
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
            tbxLog.Text = DateTime.Now + "  " + message + Environment.NewLine + tbxLog.Text;
          //  tbxLog.AppendText(DateTime.Now + "  " + message + Environment.NewLine);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (userConfig1.Save())
            {
                MessageBox.Show("保存成功");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            userConfig1.Reset();
        }

        private void tbxLog_TextChanged(object sender, EventArgs e)
        {
            if (tbxLog.Lines.Length > 1000)
            {
               
                int midlineposition = tbxLog.Text.IndexOf(tbxLog.Lines[500]);
                MessageBox.Show(midlineposition.ToString());
                tbxLog.Text = tbxLog.Text.Remove(midlineposition);
            }
        }       
    }
}
