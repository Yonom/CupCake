using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class LevelUpReceiveEvent : ReceiveEvent
    {
        public LevelUpReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.NewClass = message.GetInteger(1);
        }

        public int NewClass { get; private set; }
        public int UserId { get; private set; }
    }
}