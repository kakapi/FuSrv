using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using log4net;
using FuLib;
namespace FuSrvOC
{
    /// <summary>
    /// ftp上传文件
    /// </summary>
    public class Uploader
    {
        public static void UploadFiles()
        {
            Guid operationId = Guid.NewGuid();
            Logger.MyLogger.Debug("开始扫描"+operationId);
            try
            {
                new SiteVariables().Init();

              

                IList<LocalCallRec> records=DbUnit.GetRecordsToBeUpload(
                    new UploadLogger().GetLastUploadedFileIndex());

               
                foreach (LocalCallRec call in  records)
                {
                    bool result = UploadSingleFile(call);
                }
            }
            catch (Exception ex)
            {
                Logger.MyLogger.Fatal("*******ERROR**" + ex.Message+ex.StackTrace);
            }
            Logger.MyLogger.Debug("操作结束" + operationId);
        }


        public static bool UploadSingleFile(LocalCallRec call)
        {
            return UploadSingleFile(call.FileSavePath,call.Id, SiteVariables.FtpServerPath
                , SiteVariables.FtpUserId, SiteVariables.FtpPassword);
        }

        #region Services

       

        public static bool UploadSingleFile(string fileNametouploaded,int id
            , string ftpServer
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
            string deviceNo = string.Empty, duration = string.Empty,errMsg;
            if (!ExtractInfo(fileNametouploaded, out deviceNo, out duration))
            {
                return false;
            }
            string fileName = Path.GetFileName(fileNametouploaded);

            string targetPath = GlobalHelper.EnsurePathEndWithSlash(ftpServer) + deviceNo + "/";
            if (!FuLib.FtpUnit.EnsureFtpPath(targetPath,
                uid,pwd,out errMsg))
           {
               Logger.MyLogger.Error("Can't Create Directory"+deviceNo+",ErrorCode:"+errMsg);
               return false;
           }
            string nowString=DateTime.Now.ToString("yyyyMMdd");
           targetPath +=  nowString+"/";
           if (!FuLib.FtpUnit.EnsureFtpPath(targetPath,
               uid, pwd, out errMsg))
           {
               Logger.MyLogger.Error("Can't Create Directory" + targetPath + ",ErrorCode:" + errMsg);
               return false;
           }

           string remoteFileName = targetPath + fileName;
            string msg;
            Logger.MyLogger.Info("Begin Upload:" + fileNametouploaded);
            bool uploadResult = FuLib.FtpUnit.Upload(fileNametouploaded, remoteFileName, uid, pwd, out msg);
            Logger.MyLogger.Info("Upload Result:" + uploadResult);
            if (uploadResult == true)
            {
                Logger.MyLogger.Info(msg);
                new UploadLogger().WriteLastUploadFileIndex(id);
           

                DbUnit.UpdateRemote(deviceNo, duration,deviceNo+"/"+nowString+"/"+fileName);
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
