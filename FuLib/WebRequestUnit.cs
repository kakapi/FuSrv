using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
namespace FuLib
{
    public class WebRequestUnit
    {
        public static bool CheckWebServer(string url, out string errMsg)
        {
            errMsg = string.Empty;
            bool result = false;

           

           try
           {
               var request = WebRequest.Create(GlobalHelper.EnsureUrl( url));
                WebResponse response = request.GetResponse();
                result = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return result;
        }
    }
}
