using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
namespace FuLib
{
    public class FtpUnit
    {
        private static string EnsureServerFormat(string server)
        {
            if (server.StartsWith("ftp://"))
            {
                return server;
            }
            else {
                return "ftp://" + server;
            }
        }
        public static bool Upload(string fileNametouploaded, string targetFile, string uid, string pwd, out string msg)
        {

            bool result = false;
            msg = string.Empty;
             targetFile=  EnsureServerFormat(targetFile);
             FtpWebRequest request = CreateRequest(targetFile, uid, pwd);
            request.Method = WebRequestMethods.Ftp.UploadFile;

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
                msg = response.StatusDescription;
                result = true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return result;
        }
        public static string DownloadAndRead(string remoteFile, string uid, string pwd, out string errmsg)
        {
            errmsg = string.Empty;
            remoteFile = EnsureServerFormat(remoteFile);
            string result = string.Empty;
            try
            {
                FtpWebRequest request = CreateRequest(remoteFile, uid, pwd);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
 
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                result = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
            }

            return result;
        }

        private  static FtpWebRequest CreateRequest(string path, string uid, string pwd)
        {
            path = EnsureServerFormat(path);
            var request = (FtpWebRequest)WebRequest.Create(path);
            if (!string.IsNullOrEmpty(uid))
            {
                request.Credentials = new NetworkCredential(uid, pwd);
            }
            return request;
        }
        public static bool EnsureFtpPath(string directory, string username, string password,out string errMsg)
        {

            errMsg = string.Empty;
            var request = CreateRequest(directory, username, password);
            request.Method = WebRequestMethods.Ftp.PrintWorkingDirectory; 
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    var request2 = CreateRequest(directory, username, password);
                    request2.Method = WebRequestMethods.Ftp.MakeDirectory;

                    try
                    {
                        var makeDirResponse = (FtpWebResponse)request2.GetResponse();
                    }
                    catch (WebException exx)
                    {
                       FtpWebResponse exxResp = (FtpWebResponse)exx.Response;
                       errMsg = exxResp.StatusCode.ToString();
                        return false;
                    }

                }

            }
            return true;


        }
        public static bool CheckFtpServer(string directory, string username, string password, out string errMsg)
        {
            bool result = false;
            errMsg = string.Empty;
            var request = CreateRequest(directory, username, password);
            request.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;
            try {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                result = true;
            }
            catch(Exception ex) {
                errMsg = ex.Message;
            }
            return result;
        }
    }
}
