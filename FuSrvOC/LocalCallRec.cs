using System;
using System.Collections.Generic;

using System.Text;

namespace FuSrvOC
{
   public class LocalCallRec
   {  //设备号(等同于用户名)

       public int Id { get; set; }
        public string UserId { get; set; }
       //被叫号码
        public string RemotePhoneNo { get; set; }
          
        public string Duration { get; set; }
   
        public string LocalBeginTIme { get; set; }
        public string LocalHangupTime { get; set; }
        public string CallType{ get; set; }
        public string FileSavePath { get; set; }

    }
}
