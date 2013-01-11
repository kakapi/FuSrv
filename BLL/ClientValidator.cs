using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BLL
{
    //客户端数量验证
    public class ClientCounter
    {
        private static bool ValidClients(out string clientstring)
        {
            bool result = false;
         
            clientstring = string.Empty;

            string encryptedString = string.Empty;
            try
            {
                string encryptedFilePath = "";
                 encryptedString = File.ReadAllText(encryptedFilePath);
               clientstring=   FuLib.Crypto.DecryptStringAES(encryptedString, "P@ssw0rd");
            }
            catch
            {
                result = false;
                
            }

          

            return result;
        }
        public static void GetClients()
        {
            

        }
    }
    
}
