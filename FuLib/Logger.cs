using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using log4net.Layout;
namespace FuLib
{
    public class Logger
    {
        ILog log = null;
        ILog rollLog = null;
        public ILog GetLoggerInstance()
        {

            return GetLoggerInstance( true);

        }
        public ILog GetLoggerInstance( bool isRolling)
        {
            if (isRolling)
            {

                if (rollLog == null)
                {
                    Config( isRolling);
                    rollLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                }

                return rollLog;
            }
            else
            {
                if (log == null)
                {
                    Config( isRolling);
                    log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                }
                return log;
            }
           
           
        }
        public static void Config( bool isRolling)
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.RemoveAllAppenders(); /*Remove any other appenders*/
            FileAppender fileAppender = new FileAppender();
            if (isRolling)
            {
                fileAppender = new RollingFileAppender();
               ((RollingFileAppender) fileAppender).RollingStyle = RollingFileAppender.RollingMode.Date;
            }
            fileAppender.AppendToFile = true;
            fileAppender.LockingModel = new FileAppender.MinimalLock();

            fileAppender.File = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
            PatternLayout pl = new PatternLayout();
            pl.ConversionPattern = "%d [%2%t] %-5p [%-10c] %m%n%n";
            pl.ActivateOptions();
            fileAppender.Layout = pl;
            fileAppender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(fileAppender);

        }
    }
}
