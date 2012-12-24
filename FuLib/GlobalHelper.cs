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
    }
}
