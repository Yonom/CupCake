using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class KillReceiveEvent : ReceiveEvent
    {
        public KillReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; private set; }
    }
}