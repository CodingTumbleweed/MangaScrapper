using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MangaScrapper.Core.Interface.Logging;
using System.Reflection;
using System.IO;

namespace MangaScrapper.BLL.Factory.Logging
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
                    string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                    path = path.Substring(6);   //removing "file:\\" from prefix

                    string assemblyName = ConfigurationManager.AppSettings["Logger.AssemblyName"];
                    string className = ConfigurationManager.AppSettings["Logger.ClassName"];

                    if (String.IsNullOrEmpty(assemblyName) || String.IsNullOrEmpty(className))
                        throw new ApplicationException("Missing config Key(s) for Logger");

                    Assembly assembly = Assembly.LoadFrom(Path.Combine(path, assemblyName));
                    _logger = assembly.CreateInstance(className) as ILogger;
                    //Type loggerType = assembly.GetType(className);
                    //_logger = Activator.CreateInstance(loggerType) as ILogger;

                    if (_logger == null)
                        throw new ApplicationException(
                            string.Format("Unable to instantiate ILogger class {0}/{1}",
                            assemblyName, className));
                }
                return _logger;
            }
        }
    }
}
