using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.IO;
namespace FuTest
{
   
    public class FuSrvUploaderTest
    
    {
        [Fact]
        public void uploaderTest()
        {
            Console.Write(System.Configuration.ConfigurationManager.AppSettings.Count);
        string targetPath = @"D:\test\autouploader\ftpsite\in_combat.wav";
           
          FuSrv.Uploader.UploadFiles();
           
            Assert.True(File.Exists(targetPath));
        }
    }
}
