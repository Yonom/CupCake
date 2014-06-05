using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class GodModeReceiveEvent : ReceiveEvent, IUserEvent
    {
        public GodModeReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.IsGod = message.GetBoolean(1);
        }

        public bool IsGod { get; set; }
        public int UserId { get; set; }
    }
}