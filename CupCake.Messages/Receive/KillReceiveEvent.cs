using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class KillReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        public KillReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; set; }
    }
}