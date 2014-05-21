using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class SilverCrownReceiveEvent : ReceiveEvent, IUserEvent
    {
        public SilverCrownReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; private set; }
    }
}