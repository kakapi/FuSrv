using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using log4net.Layout;
namespace FuSrv
{
    public class Logger
    {
        static ILog log;
        public static ILog MyLogger
        {
            get
            {
                if (log == null)
                {
                    Config();
                    log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                }
                return log;
            }
        }
        public static void Config()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.RemoveAllAppenders(); /*Remove any other appenders*/

            FileAppender fileAppender = new FileAppender();
            fileAppender.AppendToFile = true;
            fileAppender.LockingModel = new FileAppender.MinimalLock();
            fileAppender.File = GlobalHelper.EnsurePathEndWithSlash(SiteVariables.LocalStoragePath) +
                SiteVariables.LoggerFileName;
            PatternLayout pl = new PatternLayout();
            pl.ConversionPattern = "%d [%2%t] %-5p [%-10c] %m%n%n";
            pl.ActivateOptions();
            fileAppender.Layout = pl;
            fileAppender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(fileAppender);

        }
    }
}
