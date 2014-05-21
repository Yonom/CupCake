using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class CrownReceiveEvent : ReceiveEvent
    {
        public CrownReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; private set; }
    }
}