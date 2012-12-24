using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using log4net;
namespace FuSrv
{
    /// <summary>
    /// ftp上传文件
    /// </summary>
    public class Uploader
    {
       

        /// <summary>
        /// 确定需要上传的文件
        /// </summary>
        /// <returns></returns>
        public static string[] GetUploadedFile()
        {
            List<string> tobeUploaded = new List<string>();
            long lastUploaded = new UploadLogger().GetLastUploadedFileTime();
           
           
            string[] result = Directory.GetFiles(SiteVariables.LocalStoragePath, "*.wav", SearchOption.AllDirectories);
            foreach (string s in result)
            {
                long dt = File.GetCreationTime(s).Ticks;
                if (dt > lastUploaded)
                {
                    tobeUploaded.Add(s);
                }
            }
            return tobeUploaded.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftpserver">ftpserver地址(包括目录名称)</param>
        /// <param name="uid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="sourceFile">需要上传的文件</param>
        public Uploader()
        {

        }
        public static void UploadFiles()
        {
            foreach (string filename in GetUploadedFile())
            {
               bool result=  UploadSingleFile(filename);
               if (result == true)
               { 
                //写入远程数据库
               }
            }
        }
        public static bool UploadSingleFile(string fileNametouploaded)
        {
            string fileName = Path.GetFileName(fileNametouploaded);
            string remoteFileName = GlobalHelper.EnsurePathEndWithSlash(SiteVariables.FtpServerPath) + fileName;
            string uid = SiteVariables.FtpUserId;
            string pwd = SiteVariables.FtpPassword;
            string msg;
            Logger.MyLogger.Info("Begin Upload:" + fileNametouploaded);
            bool uploadResult = FuLib.FtpUnit.Upload(fileNametouploaded, remoteFileName, uid, pwd, out msg);
            if (uploadResult == true)
            {
                Logger.MyLogger.Info(msg);
                new UploadLogger().WriteLastUploadFileTime(File.GetCreationTime(fileNametouploaded).Ticks);
                string deviceNo=string.Empty, duration=string.Empty;
                ExtractInfo(fileNametouploaded, out deviceNo, out duration);
                UpdateRemoteDB.Update(deviceNo,duration);
            }
            else
            {
                Logger.MyLogger.Error(msg);
            }
            return uploadResult;
        }
        private static bool ExtractInfo(string fileFullName,out string deviceno,out string duration)
        {
            bool result = false;
            deviceno = string.Empty;
            duration = string.Empty;
           string fileName = Path.GetFileName(fileFullName);

           NAudio.Wave.WaveFileReader wf = new NAudio.Wave.WaveFileReader(fileFullName);
            TimeSpan tp= wf.TotalTime;
            duration = tp.TotalSeconds.ToString();
            string[] arr = fileName.Split('_');
            deviceno = arr[0];
            return result;
        }

    }
}
