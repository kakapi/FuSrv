using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FuLib;
namespace FuTest.FuLibTest
{
    public class FTPUnitTest
    {
        [Fact]
        public void FtpDirectoryExistsTest()
        {
            string errMsg;
        
         bool resut= FtpUnit.EnsureFtpPath("ftp://127.0.0.1/sdfdsafdasf/dd/","21", "", "",out errMsg);
         Assert.True(resut);
            
        }
    }
}
