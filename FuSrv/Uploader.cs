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
        /// 本地文件存储路径
        /// </summary>
       

        /// <summary>
        /// 确定需要上传的文件
        /// </summary>
        /// <returns></returns>
        public static string[] GetUploadedFile()
        {
            List<string> tobeUploaded = new List<string>();
            DateTime? _lastUploaded = new UploadLogger().GetLastUploadedFileTime();
            if (_lastUploaded == null)
            { return new string[] { }; }
            DateTime lastUploaded = _lastUploaded.Value;

            string[] result = Directory.GetFiles(SiteVariables.LocalStoragePath, "*.wav", SearchOption.AllDirectories);
            foreach (string s in result)
            {
                DateTime dt = File.GetCreationTime(s);
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
                UploadSingleFile(filename);
            }
        }
        public static bool UploadSingleFile(string fileNametouploaded)
        {
           
            bool result = false;
            Logger.MyLogger.Info("Begin Upload");
            string fileName = Path.GetFileName(fileNametouploaded);
            string remoteFileName =GlobalHelper.EnsurePathEndWithSlash(SiteVariables.FtpServerPath) + fileName;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remoteFileName);//"ftp://www.contoso.com/test.htm");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            if (!string.IsNullOrEmpty(SiteVariables.FtpUserId))
            {
                request.Credentials = new NetworkCredential(SiteVariables.FtpUserId, SiteVariables.FtpPassword);
            }
            try
            {
                StreamReader sourceStream = new StreamReader(fileNametouploaded);
                byte[] fileContents = Encoding.Default.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                response.Close();
                Logger.MyLogger.Info(response.StatusDescription);
                new UploadLogger().WriteLastUploadFileTime(File.GetCreationTime(fileNametouploaded));
                result = true;
            }
            catch (Exception ex)
            {
                Logger.MyLogger.Error(ex.Message);
            }
            return result;
        }

    }
}
