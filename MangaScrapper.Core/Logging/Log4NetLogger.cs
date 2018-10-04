using System;
using System.IO;
using log4net;
using MangaScrapper.Core.Model.Enum;

namespace MangaScrapper.Core.Logging
{
    /// <summary>
    /// Adpator Class for Logging using Log4Net
    /// </summary>
    internal class Log4NetLogger : ILogger
    {
        private static volatile Log4NetLogger _instance;
        private static object objLock = new Object();
        private static ILog logger = null;

        public Log4NetLogger()
        {
            log4net.GlobalContext.Properties["LogFileName"] = Path.Combine(DomainSettings.AppFolder, "Logs");
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
        /// <param name="ex">Exception to be logged</param>
        public void WriteLog(string logName, LogLevel level, string message, Exception ex = null)
        {
           
        }

        /// <summary>
        /// Writes messages to log
        /// </summary>
        /// <remarks>
        /// This method works as mediator between Log4Net
        /// Library and User Code.
        /// </remarks>
        /// <param name="level">Log Level of Message</param>
        /// <param name="message">Message to be logged</param>
        /// <param name="logName">(Optional) Log Name, otherwise class name will be used as default name</param>
        /// <param name="ex">(Optional) Exception to be logged</param>
        public void WriteLog(LogLevel level, string message, string logName = null, Exception ex = null)
        {
            if (string.IsNullOrEmpty(logName))
                logName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString();
            // Gets the Log
            ILog log = LogManager.GetLogger(logName);
            string exMessage = ex == null ? string.Empty : ex.ToString();


            switch (level)
            {
                case LogLevel.FATAL:
                    if (log.IsFatalEnabled)
                        log.Fatal(message, ex);
                    break;
                case LogLevel.ERROR:
                    if (log.IsErrorEnabled)
                        log.Error(message, ex);
                    break;
                case LogLevel.WARN:
                    if (log.IsWarnEnabled)
                        log.Warn(message, ex);
                    break;
                case LogLevel.INFO:
                    if (log.IsInfoEnabled)
                        log.Info(message, ex);
                    break;
                case LogLevel.VERBOSE:
                    if (log.IsDebugEnabled)
                        log.Debug(message, ex);
                    break;
            }
        }
    }
}
