using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class ModModeReceiveEvent : ReceiveEvent, IUserEvent
    {
        public ModModeReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; set; }
    }
}