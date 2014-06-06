using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class SilverCrownReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        public SilverCrownReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; set; }
    }
}