using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class GodModeReceiveEvent : ReceiveEvent
    {
        public GodModeReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.IsGod = message.GetBoolean(1);
        }

        public bool IsGod { get; private set; }
        public int UserId { get; private set; }
    }
}