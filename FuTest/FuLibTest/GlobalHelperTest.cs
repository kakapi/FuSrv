using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FuLib;
using System.IO;
namespace FuTest.FuLibTest
{
  public  class GlobalHelperTest
    {
      [Fact]
      public void BuildFtpPathTest()
      {
          string path = "ftp://127.0.0.1/callservice";
          
          Assert.Equal("ftp://127.0.0.1:21/callservice",FuLib.GlobalHelper.BuildFtpPath(path,"21"));
      }
    }
}
