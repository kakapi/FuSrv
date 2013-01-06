using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
namespace FuLib
{
    //客户端
    public class FuTcpClient
    {
        public string ServerIP
        {
             get;set;
        }
        public int Port=13000;
        public static bool CanCommunicateWithServer(string serverIp,string msg,out string errMsg)
        {
            errMsg = string.Empty;
            bool can = false;
            TcpClient client = new TcpClient(serverIp, 13000);
            try
            {
                Stream s = client.GetStream();
                StreamWriter sw = new StreamWriter(s);
                StreamReader sr = new StreamReader(s);
                sw.AutoFlush = true;

                string dd = sr.ReadLine();

                if (dd == "OK")
                {
                    can = true;
                    sw.WriteLine(msg);
                    sw.WriteLine(string.Empty);
                }
               
                s.Close();
                
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                client.Close();
            }
            return can;
        }

        TcpClient client;
        public TcpClient GetInstance(string serverIP)
        {
            if (client == null)
            {
                client = new TcpClient(serverIP, 13000);
            }
            return client;
        }
        public FuTcpClient()
        { 
            
        }
      //  public void Send
        

    }
}
