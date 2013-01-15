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
            Logger.MyLogger.Info("###开始扫描" + operationId);
            FuSocket fusocket = new FuSocket(SiteVariables.SocketPort);
            try
            {
                 
                IList<LocalCallRec> records =DbUnit.GetRecordsToBeUpload(
                    new UploadLogger().GetLastUploadedFileIndex());
                Logger.MyLogger.Info("需要处理的通话记录数量:" + records.Count);
                
                foreach (LocalCallRec call in records)
                {
                    if (records.IndexOf(call) == 0)
                    { 
                     //检验客户端合法性.
                        fusocket.ClientActions(SiteVariables.ServerIP, ValidClient);

                        if (!isClientValid)
                        {
                            Logger.MyLogger.Info("不是有效的设备号,服务停止.");
                            fusocket.ClientActions(SiteVariables.ServerIP, ValidError);
                            if (SiteVariables.ServiceTimer != null) { SiteVariables.ServiceTimer.Stop(); }
                            break;
                        }
                    }

                    currentUploadFile = call.FileSavePath;
                    currentDeviceNo = call.DeviceNo;
                    Logger.MyLogger.Info("开始处理:" + currentUploadFile);
                    bool result = UploadSingleFile(call);
                    fusocket.ClientActions(SiteVariables.ServerIP, SendUploadMsg);
                }
            }
            catch (Exception ex)
            {
                Logger.MyLogger.Error("###" + ex.Message + ex.StackTrace);
                fusocket.ClientActions(SiteVariables.ServerIP, SendUploadError);
            }
            Logger.MyLogger.Info("###操作结束" + operationId);
        }
        static string currentDeviceNo;
        static string currentUploadFile;
        private static void SendUploadMsg(StreamReader sr, StreamWriter sw)
        {
            string status = sr.ReadLine();
            if (status == "OK")
            {
                sw.WriteLine("uploadmsg");
                sw.Flush();
                sw.WriteLine(string.Format("设备:{0}已上传文件-{1}:", currentDeviceNo, currentUploadFile));
            }
        }
        static bool isClientValid = false;
        private static void ValidClient(StreamReader sr, StreamWriter sw)
        {
            string status = sr.ReadLine();
            if (status == "OK")
            {
                sw.WriteLine("validclient");
                sw.Flush();
              string result=  sr.ReadLine();
              if (result.ToLower() == "true")
              {
                  isClientValid = true;
              }
              else
              {
                  isClientValid = false;
              }
            }
        }

        private static void SendUploadError(StreamReader sr, StreamWriter sw)
        {
            string status = sr.ReadLine();
            if (status == "OK")
            {
                sw.WriteLine("uploadmsg");
                sw.Flush();
                sw.WriteLine(string.Format("设备:{0}上传失败-{1}", currentDeviceNo, currentUploadFile));
            }
        }
        private static void ValidError(StreamReader sr, StreamWriter sw)
        {
            string status = sr.ReadLine();
            if (status == "OK")
            {
                sw.WriteLine("uploadmsg");
                sw.Flush();
                sw.WriteLine(string.Format("设备:{0}不是合法客户端,上传被拒绝", currentDeviceNo));
            }
        }

        public static bool UploadSingleFile(LocalCallRec call)
        {
            string deviceNo = call.DeviceNo;
            string targetPath;
            if (EnsureRemotePath(deviceNo, SiteVariables.FtpServerPath,SiteVariables.FtpPort
                , SiteVariables.FtpUserId, SiteVariables.FtpPassword, out targetPath))
            {
                return UploadSingleFile(call.FileSavePath,SiteVariables.FtpPort, call.Id, deviceNo, targetPath
               , SiteVariables.FtpUserId, SiteVariables.FtpPassword);
            }
            return false;

        }
        /// <summary>
        /// 确保远程路径存在
        /// </summary>
        private static bool EnsureRemotePath(string deviceNo, string ftpServer,string port, string uid, string pwd, out string targetPath)
        {
            string errMsg;
            if (!ftpServer.EndsWith("/"))
            {
                ftpServer = ftpServer + "/";
            }
            targetPath = GlobalHelper.EnsurePathEndWithSlash(ftpServer);
            string nowString = DateTime.Now.ToString("yyyyMMdd");
            targetPath += nowString + "/";
            if (!FuLib.FtpUnit.EnsureFtpPath(targetPath,port,
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
                if (!FuLib.FtpUnit.EnsureFtpPath(targetPath,port,
                   uid, pwd, out errMsg))
                {
                    Logger.MyLogger.Error("Can't Create Directory " + deviceNo + ",ErrorCode:" + errMsg);
                    return false;
                }
            }



            return true;
        }
        #region Services



        public static bool UploadSingleFile(string fileNametouploaded,string port, int id, string deviceNo
            , string targetPath
            , string uid, string pwd)
        {
            if (!File.Exists(fileNametouploaded))
            {
                Logger.MyLogger.Error("录音文件不存在:" + fileNametouploaded);
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
            Logger.MyLogger.Info("Ftp Begin Upload:" + fileNametouploaded);
            bool uploadResult = FuLib.FtpUnit.Upload(fileNametouploaded,port, remoteFileName,uid, pwd, out msg);
            Logger.MyLogger.Info("Ftp Upload Result:" + uploadResult + "," + msg);
            if (uploadResult == true)
            {

                new UploadLogger().WriteLastUploadFileIndex(id);
                string nowString = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                uploadResult = DbUnit.UpdateRemote(deviceNo, duration, nowString + "/" + deviceNo + "/" + fileName, "", "", out msg);
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
