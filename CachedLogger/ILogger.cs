using System;
using System.Collections.Generic;
using System.Text;

namespace CachedLoggerHost
{
    public interface ILogger
    {
        void Log(LogMessage message);
    }

    /// <summary>
    /// Logs messages via log4net
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        //private ILogger logger = new ConsoleLogger();

        ///// <param name="message"></param>
        //public void Log(LogMessage message)
        //{
        //    logger.Info($"Severity:{message.Type}, Message: {message.Message}{Environment.NewLine}");
        //}
        public void Log(LogMessage message)
        {
            ILogger log4NetLogger = new ConsoleLoggerLog4Net();
            log4NetLogger.Log(message);
            
        }
    }

    /// <summary>
    /// Logs messages to the console
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void Log(LogMessage message)
        {
            Console.WriteLine($"Severity:{message.MessageSeverity}, Message: {message.Message}{Environment.NewLine}");
        }
    }

    public class ConsoleLoggerLog4Net : ILogger
    {
        public void Log(LogMessage message)
        {
            Console.WriteLine($"LOG4NET::Severity:{message.MessageSeverity}, Message: {message.Message}{Environment.NewLine}");
        }
    }
}
