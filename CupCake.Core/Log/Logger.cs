namespace CupCake.Core.Log
{
    public class Logger
    {
        public Logger(LogPlatform logPlatform, string name)
        {
            this.LogPlatform = logPlatform;
            this.Name = name;
        }

        public LogPlatform LogPlatform { get; private set; }
        public string Name { get; set; }

        public void Log(string message)
        {
            this.Log(LogPriority.Message, message);
        }

        public void Log(LogPriority priority, string message)
        {
            this.LogPlatform.Log(this.Name, priority, message);
        }
    }
}