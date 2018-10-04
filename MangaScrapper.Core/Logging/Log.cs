using MangaScrapper.Core.Model.Enum;
using System;

namespace MangaScrapper.Core.Logging
{
    public static class Log
    {
        static ILogger logger = LoggerFactory.GetLogger();

        public static void Info(string message, string logName = null)
        {
            logger.WriteLog(LogLevel.INFO, message, logName);
        }

        public static void Error(string message)
        {
            logger.WriteLog(LogLevel.ERROR, message);
        }

        public static void Error(string message, string logName = null)
        {
            logger.WriteLog(LogLevel.ERROR, message, logName);
        }

        public static void Error(string message, Exception ex = null)
        {
            logger.WriteLog(LogLevel.ERROR, message, null, ex);
        }

        public static void Error(string message, string logName = null, Exception ex = null)
        {
            logger.WriteLog(LogLevel.ERROR, message, logName, ex);
        }

        public static void Fatal(string message)
        {
            logger.WriteLog(LogLevel.FATAL, message);
        }

        public static void Fatal(string message, string logName = null)
        {
            logger.WriteLog(LogLevel.FATAL, message, logName);
        }

        public static void Fatal(string message, Exception ex = null)
        {
            logger.WriteLog(LogLevel.FATAL, message, null, ex);
        }

        public static void Fatal(string message, string logName = null, Exception ex = null)
        {
            logger.WriteLog(LogLevel.FATAL, message, logName, ex);
        }
        
    }
}
