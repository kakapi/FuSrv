using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace FUServer
{
    public class ServerInfo
    {
        public static string configFileName = AppDomain.CurrentDomain.BaseDirectory + "conf.ig";

        public static bool CheckConfigOK()
        {
            return !string.IsNullOrEmpty(GetDecryptedInfo());
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
        public static void EnsureFileExists()
        {

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
