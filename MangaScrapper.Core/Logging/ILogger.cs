using System;
using MangaScrapper.Core.Model.Enum;

namespace MangaScrapper.Core.Logging
{
    /// <summary>
    /// Defines a common logging interface specification
    /// </summary>
    public interface ILogger
    {
        //void WriteLog(string logName, LogLevel level, string message);
        void WriteLog(LogLevel level, string message, string logName = null, Exception ex = null);
    }
}
