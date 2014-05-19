using CupCake.Core.Services;
using CupCake.Log.Log;

namespace CupCake.Log.Services
{
    public class LogService : CupCakeService
    {
        protected override void Enable()
        {
        }

        public void Log(string source, LogPriority priority, string message)
        {
            this.EventsPlatform.Event<LogEventArgs>().Raise(this, new LogEventArgs(source, priority, message));
        }
    }
}