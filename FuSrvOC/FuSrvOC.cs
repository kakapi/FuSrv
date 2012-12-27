using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using FuSrv;
namespace FuSrvOC
{
    public partial class FuSrvOC : ServiceBase
    {
        public FuSrvOC()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string strInteral = SiteVariables.InteralDuration;
            int interal;
            if(!int.TryParse(strInteral,out interal))
            {
                Logger.MyLogger.Error("间隔时长不是数字.请检查配置文件");
                return;
            }

            System.Timers.Timer t = new System.Timers.Timer(1000*60*interal);
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            t.Start();
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
