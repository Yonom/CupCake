using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class GodModeReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        public GodModeReceiveEvent(Message message)
            : base(message)
        {
            UserId = message.GetInteger(0);
            IsGod = message.GetBoolean(1);
        }

        public bool IsGod { get; set; }
        public int UserId { get; set; }
    }
}