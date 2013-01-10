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
            if (ServerInfo.CheckConfigOK())
            {
                started = true;
                StartService();
               
            }
            else {
                started = false;
                Log("尚未配置,服务未启动");
               
            }
            SetUIStatus();
        }
       
        private void SetUIStatus()
        {
            btnStart.Enabled = !started;
            btnStop.Enabled = started;
            if (started)
            {
                lblServerState.ForeColor = Color.ForestGreen;
                lblServerState.Text = "服务已启动";
                tbMain.SelectedIndex = 0;
                btnStart.Text = "已启动";
                btnStop.Text = "停止";
            }
            else
            {
                lblServerState.ForeColor = Color.Red;
                lblServerState.Text = "尚未配置,服务未启动";
                tbMain.SelectedIndex = 1;
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
            Log("服务已启动");
           
            
        }
        void msgHandler(string msg)
        {
            Log(msg);
        }

    

        private void btnStop_Click(object sender, EventArgs e)
        {
            started = false;
            new FuLib.FuSocket().StopServer();

            SetUIStatus();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartService();
            started = false;
            SetUIStatus();
        }

        private void Exit()
        {
            new FuLib.FuSocket().StopServer();

            Application.Exit();
        }
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
          

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
                Log("已保存配置");
                if (!started)
                {
                    
                    StartService();
                    started = true;
                    SetUIStatus();
                }
            }
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            userConfig1.Reset();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
        }

       

        private void systrayicon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Activate();
            WindowState = FormWindowState.Normal;
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void tsmConfig_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Activate();
            WindowState = FormWindowState.Normal;
            tbMain.SelectedIndex = 1;
        }       
    }
}
