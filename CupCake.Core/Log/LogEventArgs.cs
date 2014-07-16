using System;

namespace CupCake.Core.Log
{
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(string source, LogPriority priority, string message)
        {
            Source = source;
            Priority = priority;
            Message = message;
        }

        public string Message { get; set; }
        public string Source { get; set; }
        public LogPriority Priority { get; set; }
    }
}