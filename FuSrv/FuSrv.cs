﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.IO;
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
            FileSystemWatcher fsw = new FileSystemWatcher(SiteVariables.LocalStoragePath, "*.wav");
            Logger.MyLogger.Info("Service Started "+SiteVariables.LocalStoragePath);
            fsw.Changed += new FileSystemEventHandler(fsw_Changed);
            fsw.IncludeSubdirectories = true;
            fsw.EnableRaisingEvents = true;
            //Timer timer = new Timer();
            //timer.Interval = 10 * 1000;
            //timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            //timer.Start();
        }

        void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            
            Uploader.UploadFiles();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {

          //  Uploader.UploadFiles();
           // System.IO.File.Create("d:\\start" + DateTime.Now.ToString("hhmmss") + ".txt");

        }

        protected override void OnStop()
        {
          //  System.IO.File.Create("d:\\Stop" + DateTime.Now.ToString("hhmmss") + ".txt");

        }

    }
}
