using CupCake.Core.Events;
using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public abstract class SendEvent : Event
    {
        public bool Cancelled { get; set; }

        public abstract Message GetMessage();
    }
}