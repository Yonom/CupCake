using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class LeftReceiveMessage : ReceiveMessage
    {
        public readonly int UserId;

        internal LeftReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}