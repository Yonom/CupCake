using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class GuardianModeReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        public GuardianModeReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.IsGuardian = message.GetBoolean(1);
        }

        public bool IsGuardian { get; set; }
        public int UserId { get; set; }
    }
}