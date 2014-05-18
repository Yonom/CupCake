using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class GodModeReceiveMessage : ReceiveMessage
    {
        public readonly bool IsGod;
        public readonly int UserId;

        internal GodModeReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.IsGod = message.GetBoolean(1);
        }
    }
}