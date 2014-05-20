using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class SilverCrownReceiveEvent : ReceiveEvent
    {
        public SilverCrownReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; private set; }
    }
}