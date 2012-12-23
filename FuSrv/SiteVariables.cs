using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FuSrv.Properties;
namespace FuSrv
{
    public class SiteVariables
    {
        public const string LastUploadFileName = "upload.log";
        public const string LoggerFileName = "fusrv.log";
        public const string UploaderLoggerName = "uploaderlogger";
        public static string LocalStoragePath;// = ConfigurationManager.AppSettings["LocalStoragePath"];
        public static  string FtpServerPath;//= ConfigurationManager.AppSettings["FtpServerPath"];
        public static  string FtpUserId;// =ConfigurationManager.AppSettings["FtpUserId"];
        public static  string FtpPassword;//= ConfigurationManager.AppSettings["FtpPassword"];
  
        static SiteVariables()
        {
            LocalStoragePath = Settings.Default.LocalStoragePath;
            FtpServerPath = Settings.Default.FtpServerPath;
            FtpUserId = Settings.Default.FtpUserId;
            FtpPassword = Settings.Default.FtpPassword;
        }
    }
}
