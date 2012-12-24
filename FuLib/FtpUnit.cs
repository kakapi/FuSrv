using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
namespace FuLib
{
  public  class FtpUnit
    {
      public static bool Upload(string fileNametouploaded, string targetFile, string uid, string pwd,out string msg)
      {

          bool result = false;
          msg = string.Empty;
          FtpWebRequest request = (FtpWebRequest)WebRequest.Create(targetFile);
          request.Method = WebRequestMethods.Ftp.UploadFile;
          if (!string.IsNullOrEmpty(uid))
          {
              request.Credentials = new NetworkCredential(uid,pwd);
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
      public static string DownloadAndRead(string remoteFile,string uid,string pwd,out string errmsg)
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
     
    }
}
