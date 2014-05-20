using System;
using CupCake.Core.Events;

namespace CupCake.Log
{
    public class LogEvent : Event
    {
        public LogEvent(string source, LogPriority priority, string message)
        {
            this.Source = source;
            this.Priority = priority;
            this.Message = message;
        }

        public string Message { get; set; }
        public string Source { get; set; }
        public LogPriority Priority { get; set; }
    }
}