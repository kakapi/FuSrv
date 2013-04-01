<%@ WebHandler Language="C#" Class="UpdateDbService" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Collections.Generic;
public class UpdateDbService : IHttpHandler {

    string tablename = "calllog";
     const string TableName = "calllog";
     const string deviceno = "jh1";//设备编号
     const string duration = "jh8";//通话时长
     const string recordFilePath = "jh9";//保存路径(相对于ftp根目录)
     const string callRecordTime = "jh13";//文件创建时间(客户端本地时间
    public void ProcessRequest (HttpContext context) {

       /*
        ?deviceno=12344&duration=123.1&savepath=%2fcalllogs%2f
        */
        string deviceno = context.Request["deviceno"];
        string duration = context.Request["duration"];
        string savepath =context.Server.UrlDecode(context.Request["savepath"]);
        Dictionary<string, string> columnNameValues = new Dictionary<string, string>();
        columnNameValues.Add(deviceno, deviceno);
        columnNameValues.Add(duration, duration);
        columnNameValues.Add(recordFilePath, savepath);
        columnNameValues.Add(callRecordTime, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        UpdateRemoteDb(tablename, columnNameValues);
        
    }


    private void UpdateRemoteDb(string tableName, Dictionary<string, string> columnNameValues)
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
        string insertSql_ColumnNames = string.Empty;
        string insertSql_ClumnValues = string.Empty;
        foreach (var item in columnNameValues)
        {
            insertSql_ColumnNames += item.Key + ",";
            insertSql_ClumnValues += "'" + item.Value + "',";
        }
        insertSql_ColumnNames = insertSql_ColumnNames.TrimEnd(',');
        insertSql_ClumnValues = insertSql_ClumnValues.TrimEnd(',');
        string sql = string.Format("insert into {0}({1}) values({2})",
           tableName
            , insertSql_ColumnNames
            , insertSql_ClumnValues
            );
       ExcuteSql(sql);
    }
    public static void ExcuteSql(string sql)
    {
        // List<string> serverInfo = GetRemoteDb();
        string connstr = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            ExcuteSql(sql, connstr);
        
    }
    public static void ExcuteSql(string sql, string connstr)
    {
       
            SqlConnection conn = new SqlConnection(connstr);

            SqlCommand comm = new SqlCommand(sql, conn);
            if (conn.State != System.Data.ConnectionState.Open)
            { conn.Open(); }

            comm.ExecuteNonQuery();
            conn.Close();
      
        
    }
        

    public bool IsReusable
    {
        get { return true; }
    }
}