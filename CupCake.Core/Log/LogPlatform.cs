using System;
using CupCake.Core.Log;
using MuffinFramework.Platforms;

namespace CupCake.Core
{
    public class LogPlatform : Platform
    {
        public event EventHandler<LogEventArgs> LogReceived;

        protected virtual void OnLogReceived(LogEventArgs e)
        {
            EventHandler<LogEventArgs> handler = this.LogReceived;
            if (handler != null) handler(this, e);
        }

        protected override void Enable()
        {
        }

        public void Log(string source, LogPriority priority, string message)
        {
            this.OnLogReceived(new LogEventArgs(source, priority, message));
        }
    }
}