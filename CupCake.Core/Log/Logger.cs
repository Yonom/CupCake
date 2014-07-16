namespace CupCake.Core.Log
{
    public class Logger
    {
        public Logger(LogPlatform logPlatform, string name)
        {
            LogPlatform = logPlatform;
            Name = name;
        }

        public LogPlatform LogPlatform { get; private set; }
        public string Name { get; set; }

        public void Log(LogPriority priority, string message)
        {
            LogPlatform.Log(Name, priority, message);
        }
    }
}