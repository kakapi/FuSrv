using System;
using System.Collections.Generic;
using System.Text;

namespace FuLib
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class ConfigUnit
    {
        public string DBServer { get; set; }
        public string DBDatabase { get; set; }
        public string DBTableName { get; set; }
        public string DBUserName { get; set; }
        public string DBPassword { get; set; }
        public string FtpPath { get; set; }
        public int    FtpPort { get; set; }
        public string FtpUser { get; set; }
        public string FtpPwd { get; set; }
        public string AccessPwd { get; set; }
        public int SocketPort { get; set; }

        /// <summary>
        /// 客户端设备号有效性验证的WebService
        /// </summary>
        public string ClientValidationServiceUrl { get; set; }

        public ConfigUnit()
        {
            FtpPort = 21;
            SocketPort = 13009;
            AccessPwd = "quanjiu";
            DBTableName = "calllog";
        }
        private bool Validate()
        {
            if (string.IsNullOrEmpty(DBServer) | string.IsNullOrEmpty(DBDatabase)
                | string.IsNullOrEmpty(DBTableName)

                | string.IsNullOrEmpty(FtpPath))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public string Crypt()
        {
            string original = DBServer + "|"
                            + DBDatabase + "|"
                            + DBTableName + "|"
                            + DBUserName + "|"
                            + DBPassword + ";"
                            + FtpPath + "|"
                           + FtpPort + "|"
                           + FtpUser + "|"
                           + FtpPwd + ";"

                           + AccessPwd + ";"
                           + ClientValidationServiceUrl + ";"
                           + SocketPort;
            return Crypto.EncryptStringAES(original, GlobalVariables.SharedSecret);
        }
        public string Decrypt(string encrypted, out string errMsg)
        {
            errMsg = string.Empty;
            string decrypted = string.Empty;
            try
            {
                encrypted = Crypto.DecryptStringAES(encrypted, GlobalVariables.SharedSecret);
                if (encrypted.Length != 5)
                {
                    errMsg = "格式有误:length must be 5 but " + encrypted.Length;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return decrypted;
        }

      
    }
}
