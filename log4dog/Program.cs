using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log4dog
{
    internal class Program
    {
        private readonly static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            Log4netSetAppender();
            ThreadContext.Properties["myContext"] = "Logging from Main";
            log.Debug("");
        }
        private static void Log4netSetAppender()
        {
            log4net.Appender.RollingFileAppender appender = new log4net.Appender.RollingFileAppender();
            appender.File = @"C:\ProgramData\Autodesk\Revit\Addins\Log\";
            appender.AppendToFile = true;
            appender.MaxSizeRollBackups = -1;
            appender.MaximumFileSize = "1MB";
            appender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date;
            appender.DatePattern = "yyyyMMdd-HH\".log\"";
            appender.StaticLogFileName = false;
            appender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
            appender.Layout = new log4net.Layout.PatternLayout(
                "%紀錄時間：%date " +
                "%n執行緒ID:[%thread] " +
                "%n日誌級別： %-5level " +
                "%n出錯類： [%property{myContext}] - " +
                "%n錯誤描述：%message" +
                "%newline %n");
            appender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(appender);
        }
    }
}
