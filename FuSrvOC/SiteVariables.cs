﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using System.Net.Sockets;
using Socktes;
namespace FuSrvOC
{
    public class SiteVariables
    {

        public static string DbFilePath = ConfigurationManager.AppSettings["DbFilePath"];
        public static string InteralDuration = ConfigurationManager.AppSettings["InteralDuration"];

        public static string LogFilePath = AppDomain.CurrentDomain.BaseDirectory;// ConfigurationManager.AppSettings["LogFilePath"];
        //OC means Original Client from factory
        public const string LastUploadFileName = "uploadOC.log";
        public const string LoggerFileName = "fusrvOC.log";

        public static string ServerIP = ConfigurationManager.AppSettings["ServerIP"];
        public const int Port = 13092;
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
        //本地通话数据库(access)密码
        public static string AccessPwd = ConfigurationManager.AppSettings["AccessPwd"];
        //记录本地通话数据的表名

        public const string LocalTableName = "TmCallRecTable";
        //序列号列名-->过滤已上传的数据
        public const string LocalTableNameIndexCol = "id";
        public const string TableName = "calllog";
        public const string deviceno = "jh1";//设备编号
        public const string duration = "jh8";//通话时长
        public const string recordFilePath = "jh9";//保存路径(相对于ftp根目录)
        public const string callRecordTime = "jh13";//文件创建时间(客户端本地时间)
        public const string remotePhoneNo = "jh2";//被叫号码
        public const string callType = "jh3";//被叫号码

        public void Init()
        {
            DbFilePath = ConfigurationManager.AppSettings["DbFilePath"];
            InteralDuration = ConfigurationManager.AppSettings["InteralDuration"];

            //加密文件存放的路径
            InitEncryptedContent();
        }
        private Socktes.ConnectSocket Socket_Connection;
        public void InitEncryptedContent()
        {
            Logger.MyLogger.Info("开始获取服务器信息");

            this.Socket_Connection = new Socktes.ConnectSocket();
            this.Socket_Connection.IsBlocked = false;
            this.Socket_Connection.recieve += new Socktes.RecieveEventHandler(Socket_Connection_recieve);

            Socket_Connection.Connect(ServerIP, Port);
            Socket_Connection.Send(ByesConvertor.GetBytes("FetchServerInfo"));
            // 
        }

        void Socket_Connection_recieve(object Sender, Socktes.RecieveEventArgs e)
        {
            
            string receiveMsg = ByesConvertor.BytesToString(e.Data);
            if (receiveMsg.Length > 4)
            {
                try
                {
                    Logger.MyLogger.Info("获取服务器信息");
                    string serverInfo = receiveMsg;
                    string[] ss = serverInfo.Split(';');
                    DBServiceIP = ss[0].Split('|')[0];
                    DBDataBase = ss[0].Split('|')[1];
                    DBUser = ss[0].Split('|')[2];
                    DBPwd = ss[0].Split('|')[3];

                    //ftp配置
                    FtpServerPath = ss[1].Split('|')[0];
                    FtpPort = ss[1].Split('|')[1];
                    FtpUserId = ss[1].Split('|')[2];
                    FtpPassword = ss[1].Split('|')[3];
                    Logger.MyLogger.Info("已获取");
                }
                catch (Exception ex)
                {
                    Logger.MyLogger.Error("获取服务器信息出错:"+ex.Message);
                }
            }
        }

    }
}
