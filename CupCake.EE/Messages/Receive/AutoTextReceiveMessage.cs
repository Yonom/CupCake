using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class AutoTextReceiveMessage : ReceiveMessage
    {
        public readonly string AutoText;
        public readonly int UserId;

        internal AutoTextReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.AutoText = message.GetString(1);
        }
    }
}