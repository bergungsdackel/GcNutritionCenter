using NLog;

namespace TeamDMA.Core.Logging
{
    public static class Log
    {
        public static Logger GetLogger<T>()
        {
            return LogManager.GetLogger(typeof(T).GetTypeOutput());
        }
    }
}