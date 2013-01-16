using System;
using System.Collections.Generic;
using System.Text;
using FuLib;
using System.IO;
using FUServer.Properties;
namespace FUServer
{
    /// <summary>
    /// Socket服务器相关
    /// </summary>
    public class SocketAction
    {
        /// <summary>
        /// socket通信行为委托
        /// </summary>
        /// <param name="msg"></param>
        public  delegate void delReceivedMessageHandler(string msg);
        private  delReceivedMessageHandler msgHandler;
         FuSocket  fuSocket;

        public void StartServer(delReceivedMessageHandler handler)
        {
           
             
                if (fuSocket == null)
                {
                    msgHandler = handler;
                    fuSocket = new FuSocket(GlobalVariables.SocketPort);
                }
         
            fuSocket.StartServer(ServerAction);
        }
       
        private void ServerAction(StreamReader sr, StreamWriter sw,System.Net.EndPoint endPoint)
        {
            sw.WriteLine("OK");
            string requestType = sr.ReadLine();
            switch (requestType.ToLower())
            {
                case "fetchserverinfo":

                    string serverInfo = ServerInfo.GetDecryptedInfo();
                    if (string.IsNullOrEmpty(serverInfo))
                    {
                        GlobalVariables.Logger.Error("配置信息为空,请检查");
                    }
                    sw.WriteLine(serverInfo);
                   
                    break;
                case "uploadmsg":
                    string msg = sr.ReadLine();
                    msgHandler.Invoke(endPoint.ToString()+ msg);
                    break;
                case "validclient":
                   //验证客户端有效性
                    string validResult;
                    string deviceNo = sr.ReadLine();
                    string targetUrl = GlobalVariables.ClientValidationUrl.Replace("%d",deviceNo);
                    bool isValid = FuLib.WebRequestUnit.CheckWebServer(GlobalVariables.ClientValidationUrl, out validResult);
                    
                    sw.Write(validResult);
                  
                    msgHandler("客户端验证状态:"+validResult);
                    break;
              
                default: break;
            }
        }

        public void StopServer()
        {
            new FuSocket().StopServer();
        }

    }
}
