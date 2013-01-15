using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using log4net.Layout;
using FuLib;
namespace FuSrvOC
{
    public class Logger
    {
        static ILog log;
        public static ILog MyLogger
        {
            get
            {
                return new FuLib.Logger().GetLoggerInstance(AppDomain.CurrentDomain.BaseDirectory+ SiteVariables.LoggerFileName);
            }
        }
       
    }
}
