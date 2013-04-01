using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FuSrv.Properties;
using System.Data.SqlClient;
namespace FuSrv
{
    public interface IDal
    {
        void UpdateRemoteDb(string deviceno, string duration, string savePath);
    }
    
    public class DalWithHttp : IDal
    {
        public void UpdateRemoteDb(string deviceno, string duration, string savePath)
        {
            string msg;
            string urlParams=string.Format("?deviceno={0}&duration={1}&savepath={2}",deviceno,duration,savePath);
            FuLib.WebRequestUnit.CheckWebServer(SiteVariables.UpdateDataUrl+urlParams, out msg);
            Logger.MyLogger.Info("执行结果:" + msg);
        }
    }
}
