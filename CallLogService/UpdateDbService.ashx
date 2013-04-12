<%@ WebHandler Language="C#" Class="UpdateDbService" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Collections.Generic;

public class UpdateDbService : IHttpHandler
{
    //数据库连接 请根据实际的连接来进行参数配置！
    string connectionString = @"server=127.0.0.1;database=.\sqlexpress;Trusted_Connection=true;";

    public void ProcessRequest(HttpContext context)
    {

        /*
         ?deviceno=12344&duration=123.1&savepath=%2fcalllogs%2f
         */
        string deviceno = context.Request["deviceno"];
        string duration = context.Request["duration"];
        string savepath = context.Server.UrlDecode(context.Request["savepath"]);
        string strsql = string.Format("Insert Into calllog (jh1,jh8,jh9,jh13) Values ('{0}','{1}','{2}','{3}')",
            deviceno,
            duration,
            savepath,
            DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

        using (SqlConnection cn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(strsql, cn);
            cmd.CommandType = System.Data.CommandType.Text;
            cn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
        }
    }

    public bool IsReusable
    {
        get { return true; }
    }
}