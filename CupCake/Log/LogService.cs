using System;

namespace CupCake.Log
{
    public class LogService : Service
    {
        public event EventHandler<LogEventArgs> LogReceived;

        protected virtual void OnLogReceived(LogEventArgs e)
        {
            var handler = this.LogReceived;
            if (handler != null) handler(this, e);
        }

        public void Log(string source, LogPriority priority, string message)
        {
            this.OnLogReceived(new LogEventArgs(source, priority, message));
        }
    }
}