using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using log4net;
using FuLib;
namespace FuSrv
{
    /// <summary>
    /// ftp上传文件
    /// </summary>
    public class Uploader
    {
        public static void UploadFiles()
        {
           
            try
            {
                new SiteVariables().GetServiceParam();
                foreach (string filename in GetUploadedFile())
                {
                    bool result = UploadSingleFile(filename);
                }
            }
            catch (Exception ex)
            {
                Logger.MyLogger.Fatal("*******ERROR**" + ex.Message + ex.StackTrace);
            }
        }
        public static string[] GetUploadedFile()
        {
            return GetFilesToBeUploaded(FuLib.GlobalHelper.EnsurePathEndWithSlash(SiteVariables.LocalStoragePath)
                , new UploadLogger().GetLastUploadedFileTime()
                );
        }
        public static bool UploadSingleFile(string fileNametouploaded)
        {
            return UploadSingleFile(fileNametouploaded, SiteVariables.FtpServerPath
                , SiteVariables.FtpUserId, SiteVariables.FtpPassword);
        }

        #region Services

        public static string[] GetFilesToBeUploaded(string localStoragePath, long lastUploaded)
        {
            List<string> tobeUploaded = new List<string>();



            string[] result = Directory.GetFiles(localStoragePath, "*.wav", SearchOption.AllDirectories);
            foreach (string s in result)
            {
                FileInfo fi = new FileInfo(s);
                if (FuLib.IOHelper.IsFileLocked(fi))
                {
                    Logger.MyLogger.Info("文件正在被占用,暂时跳过:" + s);
                    continue;
                }
                long dt = File.GetCreationTime(s).Ticks;
                if (dt > lastUploaded)
                {
                    tobeUploaded.Add(s);
                }
            }
            return tobeUploaded.ToArray();
        }


        public static bool UploadSingleFile(string fileNametouploaded
            , string ftpServer
            , string uid, string pwd)
        {

            string deviceNo = string.Empty, duration = string.Empty, errMsg;
            if (!ExtractInfo(fileNametouploaded, out deviceNo, out duration))
            {
                return false;
            }
            string fileName = Path.GetFileName(fileNametouploaded);

            string targetPath = GlobalHelper.EnsurePathEndWithSlash(ftpServer) + deviceNo + "/";
            if (!FuLib.FtpUnit.EnsureFtpPath(targetPath,SiteVariables.FtpPort,
                uid, pwd, out errMsg))
            {
                Logger.MyLogger.Error("Can't Create Directory" + deviceNo + ",ErrorCode:" + errMsg);
                return false;
            }
            string nowString = DateTime.Now.ToString("yyyyMMdd");
            targetPath += nowString + "/";
            if (!FuLib.FtpUnit.EnsureFtpPath(targetPath, SiteVariables.FtpPort,
                uid, pwd, out errMsg))
            {
                Logger.MyLogger.Error("Can't Create Directory" + targetPath + ",ErrorCode:" + errMsg);
                return false;
            }

            string remoteFileName = targetPath + fileName;
            string msg;
            Logger.MyLogger.Info("Begin Upload:" + fileNametouploaded);
            bool uploadResult = FuLib.FtpUnit.Upload(fileNametouploaded, SiteVariables.FtpPort, remoteFileName, uid, pwd, out msg);
            Logger.MyLogger.Info("Upload Result:" + uploadResult);
            if (uploadResult == true)
            {
                Logger.MyLogger.Info(msg);
                new UploadLogger().WriteLastUploadFileTime(File.GetCreationTime(fileNametouploaded).Ticks);


                UpdateRemoteDB.Update(deviceNo, duration, deviceNo + "/" + nowString + "/" + fileName);
            }
            else
            {
                Logger.MyLogger.Error(msg);
            }
            return uploadResult;
        }
        public static bool ExtractInfo(string fileFullName, out string deviceno, out string duration)
        {
            deviceno = string.Empty;
            duration = string.Empty;
            string fileName = Path.GetFileName(fileFullName);
            string[] arr = fileName.Split('_');
            if (arr.Length != 2)
            {
                Logger.MyLogger.Info("文件名不符合规范,Skip." + fileName);
                return false;
            }
            deviceno = arr[0];

            NAudio.Wave.WaveFileReader wf = new NAudio.Wave.WaveFileReader(fileFullName);
            TimeSpan tp = wf.TotalTime;
            duration = tp.TotalSeconds.ToString();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>

    }
        #endregion
}
