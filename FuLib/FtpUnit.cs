using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
namespace FuLib
{
    public class FtpUnit
    {

        public static bool Upload(string fileNametouploaded, string targetFile, string uid, string pwd, out string msg)
        {

            bool result = false;
            msg = string.Empty;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(targetFile);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            if (!string.IsNullOrEmpty(uid))
            {
                request.Credentials = new NetworkCredential(uid, pwd);
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
            string result = string.Empty;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remoteFile);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                // This example assumes the FTP site uses anonymous logon.
                if (!string.IsNullOrEmpty(uid))
                {
                    request.Credentials = new NetworkCredential(uid, pwd);
                }

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
    }
}
