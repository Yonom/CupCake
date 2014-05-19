using CupCake.Log.Log;
using CupCake.Log.Services;

namespace CupCake.API
{
    public class Logger
    {
        public LogService LogService { get; private set; }
        public string Name { get; set; }

        public Logger(LogService logService, string name)
        {
            this.LogService = logService;
            this.Name = name;
        }

        public void Log(LogPriority priority, string message)
        {
            LogService.Log(this.Name, priority, message);
        }
    }
}
