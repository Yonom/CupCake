using CupCake.Core.Services;

namespace CupCake.Log.Services
{
    public class LogService : CupCakeService
    {
        protected override void Enable()
        {
        }

        public void Log(string source, LogPriority priority, string message)
        {
            this.Events.Raise(new LogEvent(source, priority, message));
        }
    }
}