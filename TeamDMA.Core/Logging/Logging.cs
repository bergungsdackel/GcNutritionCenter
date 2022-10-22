using log4net;
using log4net.Repository.Hierarchy;
using log4net.Core;
using log4net.Appender;
using log4net.Layout;

using TeamDMA.Core.Helper;

namespace TeamDMA.Core.Logging
{
    // TODO: Create a proper logging class
    // TODO: check if file is creating on C:
    public class Logger<T> : ILogger
    {
        ILog _logger = LogManager.GetLogger(typeof(T));

        public bool IsDebugEnabled => _logger.IsDebugEnabled;

        public bool IsInfoEnabled => _logger.IsInfoEnabled;

        public bool IsWarnEnabled => _logger.IsWarnEnabled;

        public bool IsErrorEnabled => _logger.IsErrorEnabled;

        public bool IsFatalEnabled => _logger.IsFatalEnabled;

        log4net.Core.ILogger log4net.Core.ILoggerWrapper.Logger => _logger.Logger;

        public void Debug(object message)
        {
            _logger?.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            _logger?.Debug(message, exception);
        }

        public void DebugFormat(string format, params object[] args)
        {
            _logger?.DebugFormat(format, args);
        }

        public void DebugFormat(string format, object arg0)
        {
            _logger?.DebugFormat(format, arg0);
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            _logger?.DebugFormat(format, arg0, arg1);
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            _logger?.DebugFormat(format, arg0, arg1, arg2);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            _logger?.DebugFormat(provider, format, args);
        }

        public void Error(object message)
        {
            _logger?.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            _logger?.Error(message, exception);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _logger?.ErrorFormat(format, args);
        }

        public void ErrorFormat(string format, object arg0)
        {
            _logger?.ErrorFormat(format, arg0);
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            _logger?.ErrorFormat(format, arg0, arg1);
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            _logger?.ErrorFormat(format, arg0, arg1, arg2);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            _logger?.ErrorFormat(provider, format, args);
        }

        public void Fatal(object message)
        {
            _logger?.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            _logger?.Fatal(message, exception);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _logger?.FatalFormat(format, args);
        }

        public void FatalFormat(string format, object arg0)
        {
            _logger?.FatalFormat(format, arg0);
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            _logger?.FatalFormat(format, arg0, arg1);
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            _logger?.FatalFormat(format, arg0, arg1, arg2);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            _logger?.FatalFormat(provider, format, args);
        }

        public ILog GetLogger()
        {
            ILog logger = LogManager.GetLogger(typeof(T));
            return logger;
        }

        public void Info(object message)
        {
            _logger?.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            _logger?.Info(message, exception);
        }

        public void InfoFormat(string format, params object[] args)
        {
            _logger?.InfoFormat(format, args);
        }

        public void InfoFormat(string format, object arg0)
        {
            _logger?.InfoFormat(format, arg0);
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            _logger?.InfoFormat(format, arg0, arg1);
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            _logger?.InfoFormat(format, arg0, arg1, arg2);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            _logger?.InfoFormat(format, args);
        }

        public Logger()
        {
            //var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            //XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            // CONFIG
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%-8level [%date{ISO8601}] [%logger] [thread '%thread']: %message%newline";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = true;
            string pathOfFile = Configuration.GetCurrentAppDataDir(System.Reflection.Assembly.GetCallingAssembly());
            string completePath = Path.Combine(pathOfFile, "logs", "app.log");
            roller.File = completePath;
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "10MB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Composite;
            roller.StaticLogFileName = false;
            roller.PreserveLogFileNameExtension = true;
            roller.CountDirection = 1;
            roller.MaxSizeRollBackups = -1;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            //1.OFF - nothing gets logged
            //2.FATAL
            //3.ERROR
            //4.WARN
            //5.INFO
            //6.DEBUG
            //7.ALL - everything gets logged
            hierarchy.Root.Level = Level.Debug;
            hierarchy.Configured = true;
        }

        public void Warn(object message)
        {
            _logger?.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            _logger?.Warn(message, exception);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _logger?.WarnFormat(format, args);
        }

        public void WarnFormat(string format, object arg0)
        {
            _logger?.WarnFormat(format, arg0);
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            _logger?.WarnFormat(format, arg0, arg1);
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            _logger?.WarnFormat(format, arg0, arg1, arg2);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            _logger?.WarnFormat(provider, format, args);
        }
    }
}