using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class MagicReceiveEvent : ReceiveEvent, IUserEvent
    {
        public MagicReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; set; }
    }
}