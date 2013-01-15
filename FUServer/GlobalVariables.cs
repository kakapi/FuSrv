using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using log4net;
namespace FUServer
{
    public class GlobalVariables
    {
        public static int SocketPort;

        public const string CallLogTableName = "calllog";
       static string logFileName =AppDomain.CurrentDomain.BaseDirectory+ "log/FuServer.log";
       public static bool IsRegisted = false;
       public static string SerialFileFullName = AppDomain.CurrentDomain.BaseDirectory + "serial.no";
       public static string MachineCode =FuLib.ServerInfo.GetMacAddress()+ FuLib.ServerInfo.GetCPUId();
       public static ILog Logger {
           get {
               return new FuLib.Logger().GetLoggerInstance( logFileName);
           }
       }

        public static bool GetRegistStatus()
       {
           try
           {
               string serino = File.ReadAllText(GlobalVariables.SerialFileFullName);
               string decrypted = FuLib.Crypto.DecryptStringAES(serino, "P@ssw0rd");
               if (decrypted == GlobalVariables.MachineCode)
               {
                   GlobalVariables.IsRegisted = true;
               }
               else
               {
                   Logger.Error("注册码有误.");
               }

           }
           catch (FileNotFoundException ex1)
           {
               Logger.Fatal("还未注册");
           }
           catch (Exception ex)
           {
              Logger.Fatal("Can't Read SerialNo:" + ex);
           }
           return IsRegisted;
       }

      
    }
}
