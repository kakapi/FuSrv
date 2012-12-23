using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using log4net.Layout;
namespace FuSrv
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new FuSrv()
			};

            ServiceBase.Run(ServicesToRun);
            
        }

    }
}
