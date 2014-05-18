using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class ModModeReceiveMessage : ReceiveMessage
    {
        public readonly int UserId;

        internal ModModeReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}