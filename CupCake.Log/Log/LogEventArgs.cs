using System;

namespace CupCake.Log.Log
{
    public class LogEventArgs : EventArgs
    {
        public string Message { get; set; }
        public LogPriority Priority { get; set; }

        public LogEventArgs(LogPriority priority, string message)
        {
            this.Priority = priority;
            this.Message = message;
        }
    }
}
