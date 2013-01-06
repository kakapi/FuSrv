using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.IO;
using FuSrvOC;
namespace FuSrvOCTest
{

    public class FuSrvUploaderTest
    {
        [Fact]
        public void uploaderOCTest()
        {
            Console.Write(System.Configuration.ConfigurationManager.AppSettings.Count);
            string targetPath = @"D:\test\autouploader\ftpsite\callservice\IC001\20121225\IC001_alarm1.wav";

            FuSrvOC.Uploader.UploadFiles();

            Assert.True(File.Exists(targetPath));
        }
        [Fact]
        public void uploaderTest()
        {
            Console.Write(System.Configuration.ConfigurationManager.AppSettings.Count);
            string targetPath = @"D:\test\autouploader\ftpsite\callservice\IC001\20121225\IC001_alarm1.wav";

            FuSrv.Uploader.UploadFiles();

            Assert.True(File.Exists(targetPath));
        }
        [Fact]
        public void GetFilesToBeUploadedTest()
        {
            string[] files = FuSrv.Uploader.GetFilesToBeUploaded(@"D:\test\autouploader\localrecordedfiles\2012-12-25",0);
            Assert.Equal(2, files.Length);
        }
        [Fact]
        public void ExtractInfoTest()
        {
            string deviceno, duration;
            FuSrv.Uploader.ExtractInfo(@"D:\test\autouploader\localrecordedfiles\2012-12-25\CC1002_alarm1.wav",
              out deviceno,out duration   );
          Console.Write(duration);
        }
    }
}
