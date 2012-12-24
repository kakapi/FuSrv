using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using FuSrv.Properties;
namespace FuSrv
{
    /// <summary>
    /// 上传完成之后 更新数据库
    /// </summary>
    public class UpdateRemoteDB
    {
        public List<string> GetRemoteDb()
        {
            //地址,用户名,密码
            string msg;
            List<string> result = new List<string>();
            string remoteinf = FuLib.FtpUnit.DownloadAndRead(SiteVariables.DbStrAddr,
                SiteVariables.FtpUserId,SiteVariables.FtpPassword,out msg);
            if (!string.IsNullOrEmpty(msg))
            {
                string s = FuLib.Crypto.DecryptStringAES(remoteinf, "P@ssw0rd");
               string[] sss= s.Split('|');
               foreach (string ss in sss)
               {
                   result.Add(ss);
               }
            }
           
            return result;
        }
    }
}
