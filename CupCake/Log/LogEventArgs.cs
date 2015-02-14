using System;

namespace CupCake.Log
{
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(string source, LogPriority priority, string message)
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