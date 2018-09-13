using System;
using System.Collections.Generic;
using System.Text;

namespace CachedLoggerHost
{
    public class LogMessage
    {
        public LogMessage(string message, MessageSeverity messageSeverity)
        {
            Message = message;
            MessageSeverity = messageSeverity;
        }

        public string Message { get; }
        public MessageSeverity MessageSeverity { get; }

        public override bool Equals(object obj)
        {
            return obj is LogMessage message && Equals(message);
        }

        private bool Equals(LogMessage logMessage)
        {
            return MessageSeverity == logMessage.MessageSeverity && Message == logMessage.Message;
        }
        
        //assume the 2 feilds are not immutable
        public override int GetHashCode()
        {
            int hash = 17;  //one C# compiler for anonymous types
            hash = hash * 23 + MessageSeverity.GetHashCode();
            hash = hash * 23 + Message.GetHashCode();
            return hash;
        }
    }
}
