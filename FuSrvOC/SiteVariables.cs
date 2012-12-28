using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;

namespace FuSrvOC
{
    public class SiteVariables
    {

        public static string DbFilePath = ConfigurationManager.AppSettings["DbFilePath"];
        public static string InteralDuration = ConfigurationManager.AppSettings["InteralDuration"];
        public static string AccessPwd = ConfigurationManager.AppSettings["AccessPwd"];//加密文件存放的路径
        //OC means Original Client from factory
        public const string LastUploadFileName = "uploadOC.log";
        public const string LoggerFileName = "fusrvOC.log";
        public const string UploaderLoggerName = "uploaderloggerOC";
        //ftp服务器
        public static string FtpServerPath = "";
        public static string FtpPort = "";
        public static string FtpUserId = "";
        public static string FtpPassword = "";
        //远程数据库
        public static string DBServiceIP = "";
        public static string DBUser = "";
        public static string DBPwd = "";
        public static string DBDataBase = "";
        //记录本地通话数据的表名
        public const string LocalTableName = "TmCallRecTable";
        //序列号列名-->过滤已上传的数据
        public const string LocalTableNameIndexCol = "id";
        public const string TableName = "calllog";
        public const string deviceno = "jh1";//设备编号
        public const string duration= "jh8";//通话时长
        public const string recordFilePath= "jh9";//保存路径(相对于ftp根目录)
        public const string callRecordTime = "jh13";//文件创建时间(客户端本地时间)
        public const string remotePhoneNo = "jh2";//被叫号码
        public const string callType = "jh3";//被叫号码

        public void Init()
        { 
            
        }

    }
}
