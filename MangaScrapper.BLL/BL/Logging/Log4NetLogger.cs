using MangaScrapper.Core.Interface.Logging;
using MangaScrapper.Core.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Configuration;
using System.IO;


namespace MangaScrapper.BLL.BL.Logging
{
    /// <summary>
    /// Adpator Class for Logging using Log4Net
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        private static volatile Log4NetLogger _instance;
        private static object objLock = new Object();
        private static ILog logger = null;

        public Log4NetLogger()
        {
            string AppName = ConfigurationManager.AppSettings["AppName"];
            log4net.GlobalContext.Properties["LogFileName"] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppName, "Logs"); 
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// Writes messages to log
        /// </summary>
        /// <remarks>
        /// This method works as mediator between Log4Net
        /// Library and User Code.
        /// </remarks>
        /// <param name="logName">Name of Log to which message will be written</param>
        /// <param name="level">Log Level of Message</param>
        /// <param name="message">Message to be logged</param>
        public void WriteLog(string logName, LogLevel level, string message)
        {
            // Get the Log we are going to write this message to 
            ILog log = LogManager.GetLogger(logName);

            switch (level)
            {
                case LogLevel.FATAL:
                    if (log.IsFatalEnabled)
                        log.Fatal(message);
                    break;
                case LogLevel.ERROR:
                    if (log.IsErrorEnabled)
                        log.Error(message);
                    break;
                case LogLevel.WARN:
                    if (log.IsWarnEnabled)
                        log.Warn(message);
                    break;
                case LogLevel.INFO:
                    if (log.IsInfoEnabled)
                        log.Info(message);
                    break;
                case LogLevel.VERBOSE:
                    if (log.IsDebugEnabled)
                        log.Debug(message);
                    break;
            }
        }
    }
}
