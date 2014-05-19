using CupCake.Log.Log;
using CupCake.Log.Services;

namespace CupCake.API
{
    public class Logger
    {
        public Logger(LogService logService, string name)
        {
            this.LogService = logService;
            this.Name = name;
        }

        public LogService LogService { get; private set; }
        public string Name { get; set; }

        public void Log(LogPriority priority, string message)
        {
            this.LogService.Log(this.Name, priority, message);
        }
    }
}