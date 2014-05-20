using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class WootUpReceiveEvent : ReceiveEvent
    {
        public WootUpReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; private set; }
    }
}