using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace FuSrv
{
    /// <summary>
    /// 管理上传日志
    /// </summary>
    public class UploadLogger
    {

        private  bool LocalPathExists = false;
         public UploadLogger()
        {

            if (!Directory.Exists(SiteVariables.LocalStoragePath))
            {
                Logger.MyLogger.Error("本地路径不存在,请检查配置文件的LocalStoragePath");
                return;
            }
            string uploadLogFile = GlobalHelper.EnsurePathEndWithSlash(SiteVariables.LocalStoragePath)
                + SiteVariables.LastUploadFileName;
            if (!File.Exists(uploadLogFile))
            {
                FileInfo fi = new FileInfo(uploadLogFile);
                FileStream fs = fi.Create();
                fs.Close();
            }
            LocalPathExists = true;
        }
        public  long GetLastUploadedFileTime()
        {
            if (!LocalPathExists)
            {
                return 0;
            }
            long TimeOflastUploadedFile = 0;
            string s = File.ReadAllText(GlobalHelper.EnsurePathEndWithSlash(SiteVariables.LocalStoragePath)
                  + SiteVariables.LastUploadFileName);
            if (!string.IsNullOrEmpty(s))
            {
           
                TimeOflastUploadedFile = Convert.ToInt64(s);
            }
            return TimeOflastUploadedFile;
        }
        public  void WriteLastUploadFileTime(long tickets)
        {
            long last = GetLastUploadedFileTime();
            if (tickets <= last) return;

            File.WriteAllText(GlobalHelper.EnsurePathEndWithSlash(SiteVariables.LocalStoragePath)
                  + SiteVariables.LastUploadFileName, tickets.ToString());
        }
    }
}
