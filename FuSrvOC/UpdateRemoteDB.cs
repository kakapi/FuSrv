using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
namespace FuSrv
{
    /// <summary>
    /// 上传完成之后 更新数据库
    /// </summary>
    public class UpdateRemoteDB
    {

        public UpdateRemoteDB()
        {

        }
        public static void GetIndexsToBeUpload(int lastUploadIndex)
        {
            string sql = string.Format("select * from {0} where {1}>{2}"
                ,SiteVariables.LocalTableName,SiteVariables.LocalTableNameIndexCol, new UploadLogger().GetLastUploadedFileIndex());
            //todo
        }

        public static void Update(string deviceno, string duration,string savePath)
        {
            string sql = string.Format("insert into {0}({1},{2},{3},{4}) values('{5}','{6}','{7}','{8}')",
                SiteVariables.TableName
                , SiteVariables.col1
                , SiteVariables.col2
                ,SiteVariables.col3,
                SiteVariables.col4
                
                    , deviceno
                , duration
                ,savePath
                ,DateTime.Now.ToString("yyyy-MM-dd:HH:mm:ss")
                );
            Logger.MyLogger.Info(sql);
            ExecuteRemoteSql(sql);
        }
        public static void ExecuteRemoteSql(string sql)
        {
            // List<string> serverInfo = GetRemoteDb();
            string connstr = string.Empty;
            Logger.MyLogger.Info("RemoteSql:" + sql);
            if (!string.IsNullOrEmpty(SiteVariables.DBServiceIP))
            {
                connstr = string.Format("server={0};database={1};uid={2};pwd={3};", 
                    SiteVariables.DBServiceIP,
                    SiteVariables.DBDataBase,
                    SiteVariables.DBUser, SiteVariables.DBPwd);
                Logger.MyLogger.Info("ConnStr:" + connstr);
                ExecuteSql(new SqlConnection(connstr),new SqlCommand( sql));
            }
            else
            {
                Logger.MyLogger.Error("DataBase Info Connect Failed");
            }
        }
        public static void ExecuteLocalSql(string sql)
        {
            // List<string> serverInfo = GetRemoteDb();
            string connstr = string.Empty;
            Logger.MyLogger.Info("LocalSql:" + sql);
            if (!string.IsNullOrEmpty(SiteVariables.DBServiceIP))
            {
                connstr = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};",
                    SiteVariables.DbFilePath);
                if (!string.IsNullOrEmpty(SiteVariables.AccessPwd))
                {
                    connstr += " Jet OLEDB:Database Password="+SiteVariables.AccessPwd;
                }
                Logger.MyLogger.Info("ConnStr:" + connstr);
                ExecuteSql(new OleDbConnection(connstr), new OleDbCommand(sql));
            }
            else
            {
                Logger.MyLogger.Error("DataBase Info Connect Failed");
            }
        }
        public static void ExecuteSql(IDbConnection conn,IDbCommand comm)
        {
            try
            {
               
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
    }
}
