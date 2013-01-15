using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Management;
using System.Data.Common;
using System.Data.SqlClient;
namespace FuLib
{
    public class ServerInfo
    {
        public static string GetMacAddress()
        {
            string macAddresses = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces, thereby ignoring any
                // loopback devices etc.
                if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;

                macAddresses += nic.GetPhysicalAddress().ToString();
                break;

            }
            return macAddresses;
        }
        public static string GetCPUId()
        {

            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                break;
            }
            return cpuInfo;
        }
        public static bool CheckSqlServer(string server, string database,string tableName, string userid, string pwd, out string errMsg)
        {
            bool result = false;
            errMsg = string.Empty;
            SqlConnection sqlConn = new SqlConnection(string.Format("server={0};database={1};uid={2};pwd={3}"
                , server, database, userid, pwd));
            SqlCommand sqlComm = new SqlCommand("select top 1 * from " + tableName);
            result = CheckDbServer(sqlConn, sqlComm, out errMsg);
            return result;
        }
        private static bool CheckDbServer(DbConnection conn, DbCommand comm, out string errMsg)
        {
            errMsg = string.Empty;
            bool result = false;
            comm.Connection = conn;
            try
            {
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
                result = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return result;
        }
        public static bool CheckServer(string ftpServer, string ftpPort, string ftpUser, string ftpPwd,
        string sqlServer, string sqlDbName, string tableName, string sqlUid, string sqlPwd, out string errMsg) {
            return CheckServer(ftpServer, ftpPort, ftpUser, ftpPwd, sqlServer, sqlDbName, tableName, sqlUid, sqlPwd, string.Empty, out errMsg);

        }

        public static bool CheckServer(string ftpServer,string ftpPort, string ftpUser, string ftpPwd,
        string sqlServer, string sqlDbName,string tableName, string sqlUid, string sqlPwd, string clientValidationUrl, out string errMsg)
        {
            bool result = false;

            result = FuLib.FtpUnit.CheckFtpServer(ftpServer,ftpPort, ftpUser, ftpPwd, out errMsg);
            if (result == false)
            {
                errMsg = "无法连接Ftp:" + errMsg;

            }
            else
            {
                result = CheckSqlServer(sqlServer, sqlDbName,tableName, sqlUid, sqlPwd, out errMsg);
                if (result == false)
                {
                    errMsg = "无法连接SqlServer:" + errMsg;
                }
                else
                {
                    if (!string.IsNullOrEmpty(clientValidationUrl))
                    {
                        result = FuLib.WebRequestUnit.CheckWebServer(clientValidationUrl, out errMsg);
                        if (result == false)
                        {
                            errMsg = "无法连接客户端验证URL:" + errMsg;
                        }
                    }
                }

            }
            return result;
        }

    
    }
}
