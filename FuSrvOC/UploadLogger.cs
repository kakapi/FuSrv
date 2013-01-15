using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FuLib;
namespace FuSrvOC
{
    /// <summary>
    /// 记录已处理记录的最大Index值.
    /// </summary>
    public class UploadLogger
    {

        static readonly string UploadLoggerFilePath = AppDomain.CurrentDomain.BaseDirectory + SiteVariables.LastUploadFileName;

        public UploadLogger()
        {

            if (!File.Exists(UploadLoggerFilePath))
            {
                FileInfo fi = new FileInfo(UploadLoggerFilePath);
                FileStream fs = fi.Create();
                fs.Close();
            }

        }
        public int GetLastUploadedFileIndex()
        {

            if (!File.Exists(UploadLoggerFilePath))
            {
                return 0;
            }
            int TimeOflastUploadedFile = 0;
            string s = File.ReadAllText(UploadLoggerFilePath);
            if (!string.IsNullOrEmpty(s))
            {

                TimeOflastUploadedFile = Convert.ToInt32(s);
            }
            return TimeOflastUploadedFile;
        }
        public void WriteLastUploadFileIndex(long lastUploadIndex)
        {
            long last = GetLastUploadedFileIndex();
            if (lastUploadIndex <= last) return;

            File.WriteAllText(UploadLoggerFilePath, lastUploadIndex.ToString());
        }
    }
}
