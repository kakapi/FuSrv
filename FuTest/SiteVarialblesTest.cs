using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FuSrv;
namespace FuTest
{
    
  public  class SiteVarialblesTest
  {
      [Fact]
        public void GetParamsTest()
        {
            new SiteVariables().GetServiceParam();
            Assert.Equal(".\\sqlexpress",SiteVariables.DBServiceIP);
            Console.Write(SiteVariables.FtpServerPath);
          
            Assert.Equal("ftp://127.0.0.1",SiteVariables.FtpServerPath);
          
        }
    }
}
