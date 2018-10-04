using System;
using System.Configuration;
using System.Reflection;

namespace MangaScrapper.Core.Logging
{
    /// <summary>
    /// Factory to create Logger Object based on App Config
    /// </summary>
    public class LoggerFactory
    {
        private static ILogger _logger;
        //Mutex for thread safety
        private static object objLock = new object();

        public static ILogger GetLogger()
        {
            lock (objLock)
            {
                if (_logger == null)
                {
                    string className = ConfigurationManager.AppSettings["Logger.ClassName"];

                    if (String.IsNullOrEmpty(className))
                        throw new ApplicationException("Missing config Key for Logger");

                    Assembly assembly = Assembly.GetExecutingAssembly();
                    _logger = assembly.CreateInstance(className) as ILogger;
                    //Type loggerType = assembly.GetType(className);
                    //_logger = Activator.CreateInstance(loggerType) as ILogger;

                    if (_logger == null)
                        throw new ApplicationException(
                            string.Format("Unable to instantiate ILogger class {0}",
                            className));
                }
                return _logger;
            }
        }
    }
}
