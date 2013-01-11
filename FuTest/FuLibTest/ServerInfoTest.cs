using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace FuTest.FuLibTest
{
    public class ServerInfoTest
    {
        [Fact]
        public void GetMacAddressTest()
        {
         
            Console.WriteLine("mac:" + FuLib.ServerInfo.GetMacAddress());
            Console.WriteLine("cpuid:" + FuLib.ServerInfo.GetCPUId());
        }
    }
}
