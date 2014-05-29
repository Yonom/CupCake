using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class KillReceiveEvent : ReceiveEvent, IUserEvent
    {
        public KillReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; set; }
    }
}