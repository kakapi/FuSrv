using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace FuLib
{
   
    public class FuSocket
    {
        int port = 13009;
          static   private TcpListener listener;
        private TcpClient tcpClient;
        int maxConnection = 5;//同时连接的最大数量
        public string ErrMsg { get; internal set; }
        /// <summary>
        /// 通信行为委托
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="sw"></param>
        public delegate void delCommunicationAction(StreamReader sr, StreamWriter sw,EndPoint endpoint);
        private delCommunicationAction CommunicationAction;

        private static bool started = true;//是否启动监听
        public FuSocket() { }
        public FuSocket(int socketPort)
        {
            port = socketPort;
        }
        public void StartServer(delCommunicationAction serverAction)
        {
            CommunicationAction = serverAction;
            if (listener == null)
            {
                IPHostEntry host;
                IPAddress interNetworkIp = null;
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        interNetworkIp = ip;
                    }
                }
                IPEndPoint endpoint = new IPEndPoint(interNetworkIp, port);
                listener = new TcpListener(endpoint);
            }
                listener.Start();
                for (int i = 0; i < maxConnection; i++)
                {
                    Thread t = new Thread(new ThreadStart(Service));
                    t.Start();
                }
            
        }  

        private void Service()
        {
            while (started)
            {
                try
                {
                    bool list = listener.Server.Connected;
                    Socket soc = listener.AcceptSocket();
                    soc.ReceiveTimeout = 10*1000;
                    soc.SendTimeout = 10*1000;
                    try
                    {
                        Stream s = new NetworkStream(soc);
                        StreamReader sr = new StreamReader(s);
                        StreamWriter sw = new StreamWriter(s);
                        sw.AutoFlush = true;
                        //开始监听时服务器的行为,要保证客户端和服务端的读写行为是互补的,如果同时read会死锁.
                        CommunicationAction.Invoke(sr, sw,soc.RemoteEndPoint);
                        s.Close();
                    }
                    catch (Exception ex)
                    {
                        ErrMsg = ex.Message;
                    }
                    finally
                    {
                        soc.Close();
                    }
                }
                catch
                {}
            }
        }
   
        public void StopServer()
        {
            started = false;
            if (listener != null)
            {
                try
                {
                    listener.Server.Close();
                    listener.Stop();
                    
                }
                catch { }
            }

        }
     
        public void ClientActions(string serverIp, delCommunicationAction clientAction)
        {
            if (tcpClient == null||tcpClient.Connected==false)
            {
                tcpClient = new TcpClient(serverIp, port);
            }
            
            try
            {
                Stream s = tcpClient.GetStream();
                StreamReader sr = new StreamReader(s);
                StreamWriter sw = new StreamWriter(s);
                sw.AutoFlush = true;
                clientAction.Invoke(sr, sw,tcpClient.Client.LocalEndPoint);
                s.Close();

            }
            catch (Exception exx)
            {
                ErrMsg = exx.Message;
            }
            finally
            {
                tcpClient.Close();
            }

        }






    }
}
