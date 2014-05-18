using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class ModModeReceiveMessage : ReceiveMessage
    {
        public readonly int UserID;

        internal ModModeReceiveMessage(Message message)
            : base(message)
        {
            this.UserID = message.GetInteger(0);
        }
    }
}