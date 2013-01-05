using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
namespace FuSrv
{
    //客户端
    public class FuTcpClient
    {
        public static bool CanCommunicateWithServer(string msg)
        {
            bool can = false;
            TcpClient client = new TcpClient("192.168.1.141", 13000);
            try
            {
                Stream s = client.GetStream();
                StreamWriter sw = new StreamWriter(s);
                StreamReader sr = new StreamReader(s);

                string dd = sr.ReadToEnd();

                if (dd == "OK")
                {
                    can = true;
                    sw.Write(msg);
                }
               
                s.Close();
                
            }
            catch (Exception ex)
            {
                Logger.MyLogger.Error("连接失败:"+ex.Message);
            }
            finally
            {
                client.Close();
            }
            return can;
        }
    }
}
