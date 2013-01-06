using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
namespace FUServer
{
    public class GlobalVariables
    {
        public const int Port = 13092;
       public static bool IsRegisted = false;
       public static string SerialFileFullName = AppDomain.CurrentDomain.BaseDirectory + "serial.no";
       public static string MachineCode =FuLib.ServerInfo.GetMacAddress()+ FuLib.ServerInfo.GetCPUId();
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
                   new FuLib.Logger().GetLoggerInstance().Error("注册码有误.");
               }

           }
           catch (FileNotFoundException ex1)
           {
               new FuLib.Logger().GetLoggerInstance().Fatal("还未注册");
           }
           catch (Exception ex)
           {
               new FuLib.Logger().GetLoggerInstance().Fatal("Can't Read SerialNo:" + ex);
           }
           return IsRegisted;
       }
    }
}
