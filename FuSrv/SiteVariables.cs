using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FuSrv.Properties;
using System.Net;
using System.IO;

namespace FuSrv
{
    public class SiteVariables
    {

        public const string LastUploadFileName = "upload.log";
        public const string LoggerFileName = "fusrv.log";

        public const string UploaderLoggerName = "uploaderlogger";

        public static string LocalStoragePath = ConfigurationManager.AppSettings["LocalStoragePath"];
        public static string PwdFilePath = ConfigurationManager.AppSettings["PwdFilePath"];//加密文件存放的路径

        public static string FtpServerPath = "";
        public static string FtpPort = "";
        public static string FtpUserId = "";
        public static string FtpPassword = "";



        public static string DBServiceIP = "";
        public static string DBUser = "";
        public static string DBPwd = "";
        public static string DBDataBase = "";

        public const string TableName = "calllog";
        public const string col1 = "jh1";//设备编号
        public const string col2 = "jh8";//通话时长
        public const string col3 = "jh2";//保存路径(相对于ftp根目录)
        public const string col4 = "jh3";//文件创建时间(客户端本地时间)
        static SiteVariables()
        {
    
        }
        public void  GetServiceParam()
        {
            GetServiceParam(PwdFilePath);
        }
        public void GetServiceParam(string pwdfileUrl)
        {
            string msg = "";
           
            Uri url = new Uri(PwdFilePath);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            try
            {
                req.Method = "GET";
                StreamReader ReaderText = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                msg = ReaderText.ReadToEnd();
            }
            finally
            {
                res.Close();
            }

            try
            {
                //解密
                if (!string.IsNullOrEmpty(msg))
                {
                    string s = FuLib.Crypto.DecryptStringAES(msg, "P@ssw0rd");
                    //数据库配置
                    string[] ss = s.Split(';');
                    DBServiceIP = ss[0].Split('|')[0];
                    DBUser = ss[0].Split('|')[2];
                    DBPwd = ss[0].Split('|')[3];
                    DBDataBase = ss[0].Split('|')[1];
                    //ftp配置
                    FtpServerPath = ss[1].Split('|')[0];
                    FtpPort = ss[1].Split('|')[1];
                    FtpUserId = ss[1].Split('|')[2];
                    FtpPassword = ss[1].Split('|')[3];

                }
                else
                {
                    Logger.MyLogger.Error("Can't get db server info");
                }
            }
            catch
            {
            }
        }
    }
}
