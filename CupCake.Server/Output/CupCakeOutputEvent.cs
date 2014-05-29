using CupCake.Core.Events;

namespace CupCake.Server.Output
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