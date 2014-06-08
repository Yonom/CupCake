using CupCake.Core.Events;

namespace CupCake.HostAPI.IO
{
    public class CupCakeOutputEvent : Event
    {
        public CupCakeOutputEvent(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}