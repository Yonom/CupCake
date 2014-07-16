using CupCake.Core.Events;

namespace CupCake.HostAPI.IO
{
    public class OutputEvent : Event
    {
        public OutputEvent(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}