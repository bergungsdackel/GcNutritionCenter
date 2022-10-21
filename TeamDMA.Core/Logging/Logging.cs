using System.Reflection;
using log4net;
using log4net.Config;


namespace TeamDMA.Core.Logging
{
    // TODO: Create a proper logging class
    // TODO: check if file is creating on C:
    public static class Log
    {
        public static ILog GetLogger<T>()
        {
            ILog logger = LogManager.GetLogger(typeof(T));
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            return logger;
        }

        public static void TestLogger<T>(string message)
        {
            ILog logger = GetLogger<T>();
            logger.Debug(message);
        }
    }
}