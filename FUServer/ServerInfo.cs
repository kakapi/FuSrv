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
           if (string.IsNullOrEmpty(configInfo))
           { result = false; }
           else
           {
               if (configInfo.Split(';').Length != 4)
               {
                   result = false;
               }
               else {
                   result = true;
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
