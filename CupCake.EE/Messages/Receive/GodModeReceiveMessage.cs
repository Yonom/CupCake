using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class GodModeReceiveMessage : ReceiveMessage
    {
        public bool IsGod { get; private set; }
        public int UserId { get; private set; }

        public GodModeReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.IsGod = message.GetBoolean(1);
        }
    }
}