using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FuSrv.Properties;
using System.Data.SqlClient;
namespace FuSrv
{
    public interface IDal
    {
        void UpdateRemoteDb(string tableName,Dictionary<string,string> columnNameValues);
    }
    /// <summary>
    /// 上传完成之后 更新数据库
    /// </summary>
    public class DalWithDbConn:IDal
    {


        public void UpdateRemoteDb(string tableName, Dictionary<string, string> columnNameValues)
                {
                    ///精确到秒
                    //decimal decDuration;
                    //if (decimal.TryParse(duration, out decDuration))
                    //{
                    //    duration = decDuration.ToString("0");
                    //}
                    //else {
                    //    duration = "0";
                    //}
            string insertSql_ColumnNames=string.Empty;
            string insertSql_ClumnValues=string.Empty;
                    foreach (var item in columnNameValues)
                    {
                        insertSql_ColumnNames += item.Key + ",";
                        insertSql_ClumnValues +="'"+ item.Value + "',";
                    }
                    insertSql_ColumnNames = insertSql_ColumnNames.TrimEnd(',');
                    insertSql_ClumnValues = insertSql_ClumnValues.TrimEnd(',');
                    string sql = string.Format("insert into {0}({1}) values({2})",
                        tableName
                        , insertSql_ColumnNames
                        , insertSql_ClumnValues
                        );
                    Logger.MyLogger.Debug(sql);
                    ExcuteSql(sql);
                }
                public static void ExcuteSql(string sql)
                {
                    // List<string> serverInfo = GetRemoteDb();
                    string connstr = string.Empty;
                    Logger.MyLogger.Info("ConnStr:" + sql);
                    if (!string.IsNullOrEmpty(SiteVariables.DBServiceIP))
                    {
                        connstr = string.Format("server={0};database={1};uid={2};pwd={3};", 
                            SiteVariables.DBServiceIP,
                            SiteVariables.DBDataBase,
                            SiteVariables.DBUser, SiteVariables.DBPwd);
                        Logger.MyLogger.Info("ConnStr:" + connstr);
                        ExcuteSql(sql, connstr);
                    }
                    else
                    {
                        Logger.MyLogger.Error("DataBase Info Connect Failed");
                    }
                }
                public static void ExcuteSql(string sql, string connstr)
                {
                    try
                    {
                        SqlConnection conn = new SqlConnection(connstr);

                        SqlCommand comm = new SqlCommand(sql, conn);
                        if (conn.State != System.Data.ConnectionState.Open)
                        { conn.Open(); }

                        comm.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.MyLogger.Error(ex.Message);
                    }
                }
        
    }

    public class DalWithHttp : IDal
    {
        string url = "http://localhost/UpdateDbService.ashx?deviceno=12344&duration=123.1&savepath=%2fcalllogs%2f";
        public void UpdateRemoteDb(string tableName, Dictionary<string, string> columnNameValues)
        {
            string msg;
            FuLib.WebRequestUnit.CheckWebServer(url, out msg);
            Logger.MyLogger.Info("执行结果:" + msg);
        }
    }
}
