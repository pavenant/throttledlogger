using System;
using System.Collections.Generic;
using System.Text;

namespace CachedLoggerHost
{
    public class LogHelper : IDisposable
    {

        private static ILogger _logger;
        private static ILogger Logger
        {
            
            get
            {
                if (Manager.UseLog4Net) 
                {
                    if (_logger == null)
                    {
                        _logger = new Log4NetLogger();
                    }

                }
                else
                {
                    if (_logger == null)
                    {
                        _logger = new ConsoleLogger();
                    }

                }

                return _logger;
            }
        }

        private static ConfigManager Manager { get; } = new ConfigManager() {TimerInterval = 5000, UseLog4Net = true};
        private static readonly ThrottledLogger _throttledLogger = new ThrottledLogger(Logger, Manager);

        public static void LogMessage(LogMessage message)
        {
            _throttledLogger.Log(message);
        }

        public void Dispose()
        {
            _throttledLogger.Dispose();
        }
    }
}
