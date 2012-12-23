using System;
using System.Collections.Generic;
using System.Text;

namespace FuSrv
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
