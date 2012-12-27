using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FuLib;
namespace FuSrv
{
    /// <summary>
    /// 管理上传日志
    /// </summary>
    public class UploadLogger
    {


        public UploadLogger()
        {
            string uploadLogFile = GlobalHelper.EnsurePathEndWithSlash(Environment.CurrentDirectory)
                + SiteVariables.LastUploadFileName;
            if (!File.Exists(uploadLogFile))
            {
                FileInfo fi = new FileInfo(uploadLogFile);
                FileStream fs = fi.Create();
                fs.Close();
            }

        }
        public long GetLastUploadedFileIndex()
        {
            string loggerFile = GlobalHelper.EnsurePathEndWithSlash(Environment.CurrentDirectory)
                      + SiteVariables.LastUploadFileName;
            if (!File.Exists(loggerFile))
            {
                return 0;
            }
            long TimeOflastUploadedFile = 0;
            string s = File.ReadAllText(loggerFile);
            if (!string.IsNullOrEmpty(s))
            {

                TimeOflastUploadedFile = Convert.ToInt64(s);
            }
            return TimeOflastUploadedFile;
        }
        public void WriteLastUploadFileIndex(long lastUploadIndex)
        {
            long last = GetLastUploadedFileIndex();
            if (lastUploadIndex <= last) return;

            File.WriteAllText(GlobalHelper.EnsurePathEndWithSlash(Environment.CurrentDirectory)
                  + SiteVariables.LastUploadFileName, lastUploadIndex.ToString());
        }
    }
}
