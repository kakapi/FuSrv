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
            string uploadLogFile = GlobalHelper.EnsurePathEndWithSlash(Environment.CurrentDirectory)
                + SiteVariables.LastUploadFileName;
            if (!File.Exists(uploadLogFile))
            {
                FileInfo fi = new FileInfo(uploadLogFile);
                FileStream fs = fi.Create();
                fs.Close();
            }
            LocalPathExists = true;
        }
        public  DateTime? GetLastUploadedFileTime()
        {
            if (!LocalPathExists)
            {
                return null;
            }
            DateTime TimeOflastUploadedFile = DateTime.MinValue;
            string s = File.ReadAllText(GlobalHelper.EnsurePathEndWithSlash(SiteVariables.LocalStoragePath)
                  + SiteVariables.LastUploadFileName);
            if (string.IsNullOrEmpty(s))
            {
                TimeOflastUploadedFile = DateTime.MinValue;
            }
            else
            {
                TimeOflastUploadedFile = Convert.ToDateTime(s);
            }
            return TimeOflastUploadedFile;
        }
        public  void WriteLastUploadFileTime(DateTime dt)
        {
            DateTime last = GetLastUploadedFileTime().Value;
            if (dt <= last) return;

            File.WriteAllText(GlobalHelper.EnsurePathEndWithSlash(SiteVariables.LocalStoragePath)
                  + SiteVariables.LastUploadFileName, dt.ToString());
        }
    }
}
