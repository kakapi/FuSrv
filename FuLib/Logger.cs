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
        public ILog GetLoggerInstance(string logFile)
        {

            return GetLoggerInstance( true,logFile);

        }
        public ILog GetLoggerInstance( bool isRolling,string logFile)
        {
            if (isRolling)
            {

                if (rollLog == null)
                {
                    Config( isRolling,logFile);
                    rollLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                }

                return rollLog;
            }
            else
            {
                if (log == null)
                {
                    Config( isRolling,logFile);
                    log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                }
                return log;
            }
           
           
        }
        public static void Config( bool isRolling,string logFile)
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

            fileAppender.File = logFile;
            PatternLayout pl = new PatternLayout();
            pl.ConversionPattern = "%d %-5p %m [%2%t] %n%n";
            pl.ActivateOptions();
            fileAppender.Layout = pl;
            fileAppender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(fileAppender);

        }
    }
}
