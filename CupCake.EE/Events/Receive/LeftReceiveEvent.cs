using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class LeftReceiveEvent : ReceiveEvent, IUserEvent
    {
        public LeftReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; private set; }
    }
}