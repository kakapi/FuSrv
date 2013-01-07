using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace FuLib
{
    //客户端
    public class FuSocket
    {
        int port = 13009;
        static private TcpListener listener;
        private TcpClient tcpClient;
        int maxConnection = 5;//同时连接的最大数量
        public string ErrMsg { get; internal set; }
        public delegate void delCommunicationAction(StreamReader sr, StreamWriter sw);
        private delCommunicationAction ServerAction;

        public void StartServer(delCommunicationAction serverAction)
        {
            ServerAction = serverAction;
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
                listener.Start();
                for (int i = 0; i < maxConnection; i++)
                {
                    Thread t = new Thread(new ThreadStart(Service));
                    t.Start();
                }
            }
        }
      static  bool started = true;
        public void StopServer()
        {
            started = false;
            if (listener != null)
            {
                try
                {
                    listener.Stop();
                }
                catch { }
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
                    try
                    {
                        Stream s = new NetworkStream(soc);
                        StreamReader sr = new StreamReader(s);
                        StreamWriter sw = new StreamWriter(s);
                        sw.AutoFlush = true; // enable automatic flushing
                        ServerAction.Invoke(sr, sw);
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
        public void ClientActions(string serverIp, delCommunicationAction clientAction)
        {
            if (tcpClient == null)
            {
                tcpClient = new TcpClient(serverIp, port);
            }
            try
            {
                Stream s = tcpClient.GetStream();
                StreamReader sr = new StreamReader(s);
                StreamWriter sw = new StreamWriter(s);
                sw.AutoFlush = true;
                clientAction.Invoke(sr, sw);
                s.Close();

            }
            catch (Exception exx)
            {
                ErrMsg = exx.Message;
            }
            finally
            {
                // code in finally block is guranteed 
                // to execute irrespective of 
                // whether any exception occurs or does 
                // not occur in the try block
                tcpClient.Close();
            }

        }






    }
}
