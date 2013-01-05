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
namespace FUServer
{
    public partial class FrmMain : Form
    {
        static TcpListener listener;
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
        private  void Log(string message)
        {
            new FuLib.Logger().GetLoggerInstance().Info(message);
            tbxLog.AppendText(DateTime.Now +"  "+ message+Environment.NewLine);
        }
        
        private void StartService()
        {
            if (listener == null)
            {
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("192.168.1.141");
                listener = new TcpListener(localAddr, port);
            }
            try
            {
                listener.Start();
                started = true;
                SetButtonStatus();
                Log("服务启动");

                for (int i = 0; i < LIMIT; i++)
                {
                    Thread t = new Thread(new ThreadStart(Service));
                    t.Start();
                }
            }
            catch (Exception ex)
            {
                Log("启动出错:"+ex.Message);
            }
            
        }
        private  void Service()
        {
            try
            {
                while (true && started)
                {

                    Socket soc = listener.AcceptSocket();
                    //soc.SetSocketOption(SocketOptionLevel.Socket,
                    //        SocketOptionName.ReceiveTimeout,10000);

                    Log( string.Format("Connected: {0}",
                                             soc.RemoteEndPoint));

                    try
                    {
                        Stream s = new NetworkStream(soc);
                        StreamReader sr = new StreamReader(s);
                        StreamWriter sw = new StreamWriter(s);
                        sw.AutoFlush = true; // enable automatic flushing
                        sw.WriteLine("OK");
                        while (true)
                        {
                            string clientMsg = sr.ReadLine();
                            if(string.IsNullOrEmpty(clientMsg)) break;
                            Log(clientMsg);

                        }

                    }
                    catch (Exception e)
                    {

                    Log(e.Message);

                    }

               Log( string.Format("Disconnected: {0}", 
                                        soc.RemoteEndPoint));

                    soc.Close();
                }
            }
            catch { }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            started = false;
            listener.Stop();
            
            SetButtonStatus();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartService();
           
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            started = false;
            listener.Stop();
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
    }
}
