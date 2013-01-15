using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

using System.ServiceProcess;
using System.Text;
using FuLib;
namespace FuSrvOC
{
    public partial class yuntelservice : ServiceBase
    {
        public yuntelservice()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //初始化全局变量
            string strInteral = SiteVariables.InteralDuration;
            int interal;
           

            if(!int.TryParse(strInteral,out interal))
            {
                interal = 5;
            }
            Logger.MyLogger.Info("开始验证客户端有效性");
            FuSocket fusocket = new FuSocket();


        
           SiteVariables.ServiceTimer= new System.Timers.Timer(1000*60*interal);
           SiteVariables.ServiceTimer.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);

           SiteVariables.ServiceTimer.Start();
            
        }

        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Uploader.UploadFiles();
        }

        protected override void OnStop()
        {
        }
    }
}
