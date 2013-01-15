using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
namespace FuLib
{
    public class WebRequestUnit
    {
        public static bool CheckWebServer(string url, out string outMsg)
        {
            outMsg = string.Empty;
            bool result = false;
           try
           {
               var request = WebRequest.Create(GlobalHelper.EnsureUrl( url));
                WebResponse response = request.GetResponse();
                result = true;
                System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream());

                outMsg = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                outMsg = ex.Message;
            }
            return result;
        }
        
      
    }
}
