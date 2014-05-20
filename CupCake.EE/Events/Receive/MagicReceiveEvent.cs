using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class MagicReceiveEvent : ReceiveEvent
    {
        public MagicReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; private set; }
    }
}