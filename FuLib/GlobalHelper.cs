using System;
using System.Collections.Generic;
using System.Text;

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
        
    }
}
