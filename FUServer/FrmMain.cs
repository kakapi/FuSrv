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
        private Socktes.ConnectSocket Socket_Connection;
        private Socktes.ListenSocket Socket_Listen;

        const int LIMIT = 5; //5 concurrent clients
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
            new FuLib.Logger().GetLoggerInstance().Info(message);
            tbxLog.AppendText(DateTime.Now + "  " + message + Environment.NewLine);
        }

        private void StartService()
        {

            this.Socket_Connection = new ConnectSocket();
            this.Socket_Listen = new ListenSocket();

            this.Socket_Connection.IsBlocked = true;
            this.Socket_Connection.recieve += new RecieveEventHandler(Socket_Connection_recieve);

            this.Socket_Listen.Port = GlobalVariables.Port;
            this.Socket_Listen.accept += new AcceptEvenetHandler(Socket_Listen_accept);

            this.Socket_Listen.StratListen(true);
            started = true;
            SetButtonStatus();
            Log("服务启动");

            
        }

        void Socket_Listen_accept(object Sender, AcceptEventArgs e)
        {
            Socket_Connection.SocketHandle = e.ConnectedSocket;
        }

        void Socket_Connection_recieve(object Sender, RecieveEventArgs e)
        {
            string s = ByesConvertor.BytesToString(e.Data).TrimEnd('\0') ;
            Log("接受数据:" + s);
            if (s == "FetchServerInfo")
            {
                string serverInfo = string.Format(
              "{0}|{1}|{2}|{3};{4}|{5}|{6}|{7}"
             , Settings.Default.DbServer
             , Settings.Default.DbName
             , Settings.Default.DbUid
             , Settings.Default.DbPwd
             , Settings.Default.FtpServer
             , Settings.Default.FtpPort
             , Settings.Default.FtpUid
             , Settings.Default.FtpPwd
            );
                Socket_Connection.Send(ByesConvertor.GetBytes(serverInfo));
            }
        }
 

        private void btnStop_Click(object sender, EventArgs e)
        {
            started = false;
            Socket_Listen.StopListen();

            SetButtonStatus();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartService();

        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            started = false;

            Socket_Listen.Close();
            Application.ExitThread();

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
