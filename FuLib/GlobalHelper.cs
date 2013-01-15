using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace FuLib
{
    public class GlobalHelper
    {
        /// <summary>
        /// 确保路径以分隔符结尾(/或者\)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string EnsurePathEndWithSlash(string path)
        {


            if (path.EndsWith("/") || path.EndsWith(@"\") || path.EndsWith(@"\\"))
            { return path; }
            string end = "/";
             if (path.Contains(@"\")) { end = @"\"; }
             else if (path.Contains(@"\\")) { end = @"\\"; }

            path += end;
            return path;

        }
        public static string ParseIP(string str)
        {
            string regex = @"\b(?:\d{1,3}\.){3}\d{1,3}\b";
            string result = System.Text.RegularExpressions.Regex.Match(str, regex).Value;
            return result;
        }

        public static string EnsureUrl(string url)
        {
          return  url = url.StartsWith("http://") ? url : "http://" + url;
        }

        public static string BuildFtpPath(string path, string port)
        { 
            
           string portPatern=@"ftp://.*?:\d+";
           if (Regex.IsMatch(path, portPatern)) { return path; }
            //ftp://127.0.0.1/callservice
            string patern = "ftp://.*?/";
          
            string server=Regex.Match(path, patern).Value;
            string serverWithPort=server.Substring(0,server.Length-1)+":"+port+"/";
            string target = Regex.Replace(path, patern, serverWithPort);
            return target;
            

        }
    }
}
