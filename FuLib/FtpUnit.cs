using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
namespace FuLib
{
    public class FtpUnit
    {
        private static string EnsureServerFormat(string server,string port)
        {
            if (!server.StartsWith("ftp://"))
            {
                server = "ftp://" + server;
            }
            
           string portPatern=@"ftp://.*?:\d+";
           if (Regex.IsMatch(server, portPatern)) { return server; }
            //ftp://127.0.0.1/callservice
            string patern = "ftp://.*?/";
            int intPort;
            if (!int.TryParse(port,out intPort))
            { intPort = 21; }
            string serverPart = Regex.Match(server, patern).Value;
            string serverWithPort = serverPart.Substring(0, serverPart.Length - 1) + ":" + intPort + "/";
            string target = Regex.Replace(server, patern, serverWithPort);
            return target;
        }
        public static bool Upload(string fileNametouploaded,string port, string targetFile, string uid, string pwd, out string msg)
        {

            bool result = false;
            msg = string.Empty;
           
            FtpWebRequest request = CreateRequest(targetFile,port,uid, pwd);
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
        public static string DownloadAndRead(string remoteFile,string port, string uid, string pwd, out string errmsg)
        {
            errmsg = string.Empty;
         
            string result = string.Empty;
            try
            {
                FtpWebRequest request = CreateRequest(remoteFile,port, uid, pwd);
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

        static string ErrMsg;
        private static FtpWebRequest CreateRequest(string path,string port, string uid, string pwd)
        {
            path = EnsureServerFormat(path,port);
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(path);
                if (!string.IsNullOrEmpty(uid))
                {
                    request.Credentials = new NetworkCredential(uid, pwd);
                }
                return request;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return null;
            }

        }
        public static bool EnsureFtpPath(string directory,string port, string username, string password, out string errMsg)
        {

            errMsg = ErrMsg;
            try
            {
                var request = CreateRequest(directory,port, username, password);
                request.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    var request2 = CreateRequest(directory,port, username, password);
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
        public static bool CheckFtpServer(string directory, string port,string username, string password, out string errMsg)
        {
            bool result = false;
            errMsg = string.Empty;
            var request = CreateRequest(directory,port, username, password);
          
            try
            {
                request.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                result = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return result;
        }
    }
}
