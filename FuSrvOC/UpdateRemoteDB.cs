using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
namespace FuSrvOC
{
    /// <summary>
    /// 上传完成之后 更新数据库
    /// </summary>
    public class DbUnit
    {

        public DbUnit()
        {

        }
        private static IDbConnection _localConn;
        private static IDbConnection LocalConn
        {
            get
            {
                if (_localConn == null)
                {
                    string connstr;
                   
                    if (!string.IsNullOrEmpty(SiteVariables.DbFilePath))
                    {
                        connstr = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};",
                            SiteVariables.DbFilePath);
                        if (!string.IsNullOrEmpty(SiteVariables.AccessPwd))
                        {
                            connstr += " Jet OLEDB:Database Password=" + SiteVariables.AccessPwd;
                        }
                        _localConn = new OleDbConnection(connstr);
                        Logger.MyLogger.Info("ConnStr:" + connstr);
                    }
                    else
                    {
                        Logger.MyLogger.Error("DataBase Info Connect Failed");
                    }
                   
                }
                return _localConn;
            }

        }
        private static IDbConnection _remoteConn;
        private static IDbConnection RemoteConn
        {
            get
            {
                if (_remoteConn == null)
                {
                   
                    string connstr = string.Empty;

                    if (!string.IsNullOrEmpty(SiteVariables.DBServiceIP))
                    {
                        connstr = string.Format("server={0};database={1};uid={2};pwd={3};",
                            SiteVariables.DBServiceIP,
                            SiteVariables.DBDataBase,
                            SiteVariables.DBUser, SiteVariables.DBPwd);
                        Logger.MyLogger.Info("ConnStr:" + connstr);
                        _remoteConn = new SqlConnection(connstr);
                    }
                    else
                    {
                        Logger.MyLogger.Error("DataBase Info Connect Failed");
                    }
                    
                }
                return _remoteConn;
            }
        }

        public static IList<LocalCallRec> GetRecordsToBeUpload(int lastUploadIndex)
        {
            IList<LocalCallRec> rl = new List<LocalCallRec>();
            string sql = string.Format("select * from {0} where {1}>{2}"
                , SiteVariables.LocalTableName, SiteVariables.LocalTableNameIndexCol, new UploadLogger().GetLastUploadedFileIndex());
            IDataReader reader = ExecuteReader(LocalConn, new OleDbCommand(sql));
            while (reader.Read())
            {
                LocalCallRec lcr = new LocalCallRec();
                lcr.Id = (int)reader["ID"];
                lcr.DeviceNo = reader["序列号"].ToString();
                lcr.CallType = reader["类型"].ToString();
                lcr.Duration = reader["通话时间"].ToString();
                lcr.LocalBeginTIme = reader["开始时间"].ToString();
                lcr.LocalHangupTime = reader["结束时间"].ToString();
                lcr.RemotePhoneNo = reader["号码"].ToString();
                lcr.UserId = reader["用户名"].ToString();
                lcr.FileSavePath = reader["录音文件"].ToString();
                rl.Add(lcr);

            }
            LocalConn.Close();
            return rl;

        }
        public static void UpdateRemote(string deviceno
            , string duration
            , string recordfilelocation, string remotePhoneNo
            , string callType)
        {
            string sql = string.Format(@"insert into {0}({1},{2},{3},{4},{5},{6})
                        values('{7}','{8}','{9}','{10}','{11}','{12}')",
                SiteVariables.TableName
                , SiteVariables.deviceno
                , SiteVariables.callRecordTime
                , SiteVariables.callType,
                SiteVariables.duration,
                SiteVariables.remotePhoneNo,
                SiteVariables.recordFilePath

                    , deviceno
                   , DateTime.Now.ToString("yyyy-MM-dd:HH:mm:ss")
                    , callType
                , duration
                , remotePhoneNo
                , recordfilelocation
                
                );
            Logger.MyLogger.Info(sql);
            ExecuteSql(RemoteConn, new SqlCommand(sql));
        }
        public static void UpdateRemote(string deviceno, string duration, string savePath)
        {
            UpdateRemote(deviceno, duration, savePath,string.Empty, string.Empty);
        }


        public static void ExecuteSql(IDbConnection conn, IDbCommand comm)
        {
            try
            {
                comm.Connection = conn;
                if (conn.State != System.Data.ConnectionState.Open)
                { conn.Open(); }

                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Logger.MyLogger.Error(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public static IDataReader ExecuteReader(IDbConnection conn, IDbCommand comm)
        {
            IDataReader reader = null;
            try
            {
                comm.Connection = conn;

                if (conn.State != System.Data.ConnectionState.Open)
                { conn.Open(); }

                reader = comm.ExecuteReader();

            }
            catch (Exception ex)
            {
                Logger.MyLogger.Error(ex.Message);
            }

            return reader;
        }
    }
}
