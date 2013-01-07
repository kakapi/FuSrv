using System;
using System.Collections.Generic;
using System.Text;
using FuLib;
using System.IO;
using FUServer.Properties;
namespace FUServer
{
    public class SocketAction
    {
        public delegate void delReceivedMessageHandler(string msg);
        private delReceivedMessageHandler msgHandler;
        FuSocket fuSocket;
        public void StartServer(delReceivedMessageHandler handler)
        {
            if (fuSocket == null)
            {
                msgHandler = handler;
                fuSocket = new FuSocket();
                fuSocket.StartServer(ServerAction);
            }
        }
       
        private void ServerAction(StreamReader sr, StreamWriter sw)
        {
            sw.WriteLine("OK");
            string requestType = sr.ReadLine();
            switch (requestType.ToLower())
            {
                case "fetchserverinfo":
                    string serverInfo = string.Format(
            "{0}|{1}|{2}|{3};{4}|{5}|{6}|{7}"
           , Settings.Default.DbServer
           , Settings.Default.DbName
           , Settings.Default.DbUid
           , Settings.Default.DbPwd
           , Settings.Default.FtpServer
           , Settings.Default.FtpPort
           , Settings.Default.FtpUid
           , Settings.Default.FtpPwd
          );
                    sw.WriteLine(serverInfo);
                    break;
                case "uploadmsg":
                    string msg = sr.ReadLine();
                    msgHandler.Invoke(msg);
                    break;
                default: break;
            }
        }
    }
}
