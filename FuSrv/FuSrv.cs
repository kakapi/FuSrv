using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Timers;
namespace FuSrv
{
    public partial class FuSrv : ServiceBase
    {
        public FuSrv()
        {
            
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Timer timer = new Timer();
            timer.Interval = 10 * 1000;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            System.IO.File.WriteAllText(@"d:\test\autoupload\file1.txt", System.Configuration.ConfigurationManager.AppSettings.Count + "____"
                + Properties.Settings.Default.LocalStoragePath);
    
           // System.IO.File.Create("d:\\start" + DateTime.Now.ToString("hhmmss") + ".txt");

        }

        protected override void OnStop()
        {
            System.IO.File.Create("d:\\Stop" + DateTime.Now.ToString("hhmmss") + ".txt");

        }

    }
}
