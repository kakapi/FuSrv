using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Data.SqlClient;
namespace FUServer
{
    public class ServerInfo
    {
        public static string configFileName = AppDomain.CurrentDomain.BaseDirectory + "conf.ig";

        
        public static bool CheckConfigOK()
        {
            bool result = true;
           string configInfo=GetDecryptedInfo();
           string[] splitedInfo = configInfo.Split(';');
         
           if (string.IsNullOrEmpty(configInfo))
           { result = false; }
           else
           {
               if (splitedInfo.Length != 5)
               {
                   result = false;
               }
               else {
                   string[] dbconfig = splitedInfo[0].Split('|');
                   string[] ftpconfig = splitedInfo[1].Split('|');

                   string dbserver = dbconfig[0];
                   string dbDatabase = dbconfig[1];
                   //Properties.Settings.Default.DbServer;
                   string dbUser = dbconfig[2];
                   string dbPwd = dbconfig[3];
                   string dbCallLogTable = dbconfig[4];

                   string ftpPath = ftpconfig[0];
                   string ftpPort = ftpconfig[1];
                   string ftpUser = ftpconfig[2];
                   string ftpPwd = ftpconfig[3];

                   string accessPwd = splitedInfo[2];
                   string clientValidationUrl = splitedInfo[3];
                   string errMsg;
                   GlobalVariables.SocketPort = Convert.ToInt32(splitedInfo[4]);
                 result=  FuLib.ServerInfo.CheckServer(ftpPath, ftpPort, ftpUser, ftpPwd,
                       dbserver, dbDatabase, dbCallLogTable, dbUser, dbPwd,
                       clientValidationUrl,
                       out errMsg);
               }
           }
            return result;
          
        }
        public static string GetDecryptedInfo()
        {
            if (!File.Exists(configFileName))
            {
                return string.Empty;
            }
            string encrypted = File.ReadAllText(configFileName);
            try
            {
                string decrypted = FuLib.Crypto.DecryptStringAES(encrypted, "P@ssw0rd");
                return decrypted;
            }
            catch { return string.Empty; }
        }
     
        public static void SaveEncryptedInfo(string encryptedInfo)
        {
            if (!File.Exists(configFileName))
            {
                FileStream fs = File.Create(configFileName);
                fs.Close();
            }
            File.WriteAllText(configFileName, encryptedInfo);
        }

      
      
    }
}
