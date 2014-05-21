using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class WootUpReceiveEvent : ReceiveEvent, IUserEvent
    {
        public WootUpReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; private set; }
    }
}