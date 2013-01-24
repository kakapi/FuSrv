using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FuSrv.Properties;
using System.Data.SqlClient;
namespace FuSrv
{
    /// <summary>
    /// 上传完成之后 更新数据库
    /// </summary>
    public class UpdateRemoteDB
    {

        public UpdateRemoteDB()
        {

        }
        //public static List<string> GetRemoteDb()
        //{
        //    //地址,用户名,密码
        //    string msg;
        //    List<string> result = new List<string>();
        //    string remoteinf = FuLib.FtpUnit.DownloadAndRead(SiteVariables.DbStrAddr,
        //        SiteVariables.FtpUserId,SiteVariables.FtpPassword,out msg);
        //    Logger.MyLogger.Info("remoteInfo:"+remoteinf);

        //    if (string.IsNullOrEmpty(msg))
        //    {
        //        string s = FuLib.Crypto.DecryptStringAES(remoteinf, "P@ssw0rd");
        //        string[] sss = s.Split('|');
        //        foreach (string ss in sss)
        //        {
        //            if (string.IsNullOrEmpty(ss)) { continue; }
        //            result.Add(ss);
        //        }
        //    }
        //    else
        //    { 
        //     Logger.MyLogger.Error("Can't get db server info");
        //    }

        //    return result;
        //}

        public static void Update(string deviceno, string duration,string savePath)
        {
            ///精确到秒
            decimal decDuration;
            if (decimal.TryParse(duration, out decDuration))
            {
                duration = decDuration.ToString("0");
            }
            else {
                duration = "0";
            }

            string sql = string.Format("insert into {0}({1},{2},{3},{4}) values('{5}','{6}','{7}','{8}')",
                SiteVariables.TableName
                , SiteVariables.deviceno
                , SiteVariables.duration
                ,SiteVariables.recordFilePath,
                SiteVariables.callRecordTime
                
                    , deviceno
                , duration
                ,savePath
                ,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                );
            Logger.MyLogger.Debug(sql);
            ExcuteSql(sql);
        }
        public static void ExcuteSql(string sql)
        {
            // List<string> serverInfo = GetRemoteDb();
            string connstr = string.Empty;
            Logger.MyLogger.Info("ConnStr:" + sql);
            if (!string.IsNullOrEmpty(SiteVariables.DBServiceIP))
            {
                connstr = string.Format("server={0};database={1};uid={2};pwd={3};", 
                    SiteVariables.DBServiceIP,
                    SiteVariables.DBDataBase,
                    SiteVariables.DBUser, SiteVariables.DBPwd);
                Logger.MyLogger.Info("ConnStr:" + connstr);
                ExcuteSql(sql, connstr);
            }
            else
            {
                Logger.MyLogger.Error("DataBase Info Connect Failed");
            }
        }
        public static void ExcuteSql(string sql, string connstr)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connstr);

                SqlCommand comm = new SqlCommand(sql, conn);
                if (conn.State != System.Data.ConnectionState.Open)
                { conn.Open(); }

                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Logger.MyLogger.Error(ex.Message);
            }
        }
    }
}
