using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class MagicReceiveEvent : ReceiveEvent, IUserEvent
    {
        public MagicReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; private set; }
    }
}