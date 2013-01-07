using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FuLib;
using System.IO;
namespace FuTest.FuLibTest
{
  public  class FuSocketTest
    {
      [Fact]
      public void StartServerTest()
      {
          new FuSocket().StartServer(ServerAction);
          new FuSocket().ClientActions("127.0.0.1",ClientAction);

      }

      private void ServerAction(StreamReader sr,StreamWriter sw)
      {
          sw.WriteLine("OK");
         string requestType= sr.ReadLine();
         sw.WriteLine("your request is:"+ requestType);
      }
      private void ClientAction(StreamReader sr, StreamWriter sw)
      {
          string status = sr.ReadLine();
          sw.WriteLine("FetchServerInfo");
          string returnMsg = sr.ReadLine();
      }
    }
}
