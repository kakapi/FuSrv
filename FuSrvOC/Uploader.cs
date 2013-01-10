using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using log4net;
using FuLib;
using System.Net.Sockets;

namespace FuSrvOC
{
    /// <summary>
    /// ftp上传文件
    /// </summary>
    public class Uploader
    {
       
        public static void UploadFiles()
        {
            new SiteVariables().Init();

               Guid operationId = Guid.NewGuid();
            Logger.MyLogger.Debug("开始扫描"+operationId);
            try
            {
                IList<LocalCallRec> records=DbUnit.GetRecordsToBeUpload(
                    new UploadLogger().GetLastUploadedFileIndex());
                foreach (LocalCallRec call in  records)
                {
                    currentUploadFile = call.FileSavePath;
                    currentDeviceNo = call.DeviceNo;
                    FuSocket fusocket = new FuSocket();
                    fusocket.ClientActions(SiteVariables.ServerIP, SendUploadMsg);
                  bool result = UploadSingleFile(call);
                }
            }
            catch (Exception ex)
            {
                Logger.MyLogger.Fatal("*******ERROR**" + ex.Message+ex.StackTrace);
            }
            Logger.MyLogger.Debug("操作结束" + operationId);
        }
        static string currentDeviceNo;
        static string currentUploadFile;
        private static void SendUploadMsg(StreamReader sr,StreamWriter sw)
        {
            string status = sr.ReadLine();
            if (status == "OK")
            {
                sw.WriteLine("uploadmsg");
                sw.Flush();
                sw.WriteLine(string.Format("设备{0}上传文件{1}:",currentDeviceNo, currentUploadFile));
            }
        }

       

        public static bool UploadSingleFile(LocalCallRec call)
        {
            string deviceNo = call.DeviceNo;
            string targetPath;
            if(EnsureRemotePath(deviceNo,SiteVariables.FtpServerPath
                , SiteVariables.FtpUserId, SiteVariables.FtpPassword,out targetPath))
            {
                return UploadSingleFile(call.FileSavePath, call.Id,deviceNo,targetPath
               , SiteVariables.FtpUserId, SiteVariables.FtpPassword);
            }
            return false;
           
        }
        /// <summary>
        /// 确保远程路径存在
        /// </summary>
        private static bool EnsureRemotePath(string deviceNo,string ftpServer,string uid,string pwd,out string targetPath )
        {
            string errMsg;
            targetPath = GlobalHelper.EnsurePathEndWithSlash(ftpServer);
            string nowString = DateTime.Now.ToString("yyyyMMdd");
            targetPath += nowString + "/";
            if (!FuLib.FtpUnit.EnsureFtpPath(targetPath,
                uid, pwd, out errMsg))
            {
                Logger.MyLogger.Error("Can't Create Directory " + targetPath + ",ErrorCode:" + errMsg);
                return false;
            }
            if (string.IsNullOrEmpty(deviceNo))
            {
                Logger.MyLogger.Info("DeviceNo为空,文件将被保存在日期文件夹下.");
            }
            else
            {
                targetPath += deviceNo + "/";
                if (!FuLib.FtpUnit.EnsureFtpPath(targetPath,
                   uid, pwd, out errMsg))
                {
                    Logger.MyLogger.Error("Can't Create Directory " + deviceNo + ",ErrorCode:" + errMsg);
                    return false;
                }
            }
          
           
         
            return true;
        }
        #region Services

       

        public static bool UploadSingleFile(string fileNametouploaded,int id,string deviceNo
            , string targetPath
            , string uid, string pwd)
        {
            if (!File.Exists(fileNametouploaded))
            {
                Logger.MyLogger.Error("录音文件不存在:"+fileNametouploaded);
                return false;

            }
            if (FuLib.IOHelper.IsFileLocked(new FileInfo(fileNametouploaded)))
            {
                Logger.MyLogger.Info("文件正在被占用,跳过:" + fileNametouploaded);
            }
            string duration = string.Empty;
           

            string fileName = Path.GetFileName(fileNametouploaded);

           
           string remoteFileName = targetPath + fileName;
            string msg;
            Logger.MyLogger.Info("Begin Upload:" + fileNametouploaded);
            bool uploadResult = FuLib.FtpUnit.Upload(fileNametouploaded, remoteFileName, uid, pwd, out msg);
            Logger.MyLogger.Info("Upload Result:" + uploadResult);
            if (uploadResult == true)
            {
                Logger.MyLogger.Info(msg);
                new UploadLogger().WriteLastUploadFileIndex(id);
                string nowString = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                DbUnit.UpdateRemote(deviceNo, duration, nowString + "/" + deviceNo + "/" + fileName);
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
