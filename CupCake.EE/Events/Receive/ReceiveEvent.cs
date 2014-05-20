using System.ComponentModel;
using CupCake.Core.Events;
using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public abstract class ReceiveEvent : Event
    {
        protected ReceiveEvent(Message message)
        {
            this.PlayerIOMessage = message;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Message PlayerIOMessage { get; private set; }
    }
}