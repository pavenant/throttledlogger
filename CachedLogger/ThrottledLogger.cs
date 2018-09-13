using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace CachedLoggerHost
{
    public class ThrottledLogger : IDisposable
    {
        
        private readonly Timer _timer;
        private readonly IConfigManager _configManager;
        private ConcurrentDictionary<LogMessage,byte> _messages = new ConcurrentDictionary<LogMessage,byte>();
        private readonly ILogger _logger;

        public ThrottledLogger(ILogger logger, IConfigManager configManager)
        {
            _logger = logger;
            _configManager = configManager;
            _timer = new Timer(_configManager.TimerInterval);
            _timer.Elapsed += Callback;
            _timer.AutoReset = false;
            _timer.Start();
        }

        public void Log(LogMessage message)
        {
            if (message != null)
            {
                var added = _messages.TryAdd(message, 0);
            }
        }

        private void Callback(object source, ElapsedEventArgs e)
        {
            var messagesToLog = _messages; //reference all the messages to be logged 
            _messages = new ConcurrentDictionary<LogMessage,byte>(); //set current reference to empty new list

            if (messagesToLog.Count == 0)
            {
                return;
            }

            foreach (var message in messagesToLog.Keys)
            {
                _logger.Log(message);
            }

            Console.WriteLine(_timer.Enabled);
            if (_configManager.Refresh())
            {
                _timer.Interval = _configManager.TimerInterval;
                //change logger?
            }

            _timer.Enabled = true;

        }

        public void Dispose()
        {
            _timer?.Stop();  //I am pretty sure timer.stop disposes the timer object... https://referencesource.microsoft.com/#System/services/timers/system/timers/Timer.cs,c458cff06715bb66,references
        }
    }
}
