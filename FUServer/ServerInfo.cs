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
     
        public static void SaveEncryptedInfo(string encryptedInfo)
        {
            if (!File.Exists(configFileName))
            {
                FileStream fs = File.Create(configFileName);
                fs.Close();
            }
            File.WriteAllText(configFileName, encryptedInfo);
        }

        public static bool CheckSqlServer(string server, string database, string userid, string pwd, out string errMsg)
        { 
            bool result=false;
            errMsg = string.Empty;
            SqlConnection sqlConn = new SqlConnection(string.Format("server={0};database={1};uid={2};pwd={3}"
                ,server,database,userid,pwd));
            SqlCommand sqlComm = new SqlCommand("select top 1 * from "+GlobalVariables.CallLogTableName);
            result = CheckDbServer(sqlConn, sqlComm, out errMsg);
                return result;
        }
        private static bool CheckDbServer(DbConnection conn,DbCommand comm,out string errMsg)
        {
            errMsg = string.Empty;
            bool result = false;
            comm.Connection = conn;
            try {
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
                result = true;
            }
            catch (Exception ex){
                errMsg = ex.Message;
            }
            return result;
        }
    }
}
