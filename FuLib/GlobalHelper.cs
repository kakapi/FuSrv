using System;
using System.Collections.Generic;
using System.Text;

namespace FuLib
{
    public class GlobalHelper
    {
        public static string EnsurePathEndWithSlash(string path)
        {
           
            string end = @"\\";
            
            if (path.EndsWith("/") || path.EndsWith(@"\") || path.EndsWith(@"\\"))
            { return path; }

            if (path.Contains("/")) { end = "/"; }
            if (path.Contains(@"\\")) { end = @"\\"; }
            if (path.Contains(@"\")) { end = @"\"; }
            path += end;
            return path;

        }
        public static string ParseIP(string str)
        { 
            string regex=@"\b(?:\d{1,3}\.){3}\d{1,3}\b";
            string result = System.Text.RegularExpressions.Regex.Match(str, regex).Value;
            return result;
        }
    }
}
