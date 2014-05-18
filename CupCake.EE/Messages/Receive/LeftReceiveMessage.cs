using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class LeftReceiveMessage : ReceiveMessage
    {
        public readonly int UserID;

        internal LeftReceiveMessage(Message message)
            : base(message)
        {
            this.UserID = message.GetInteger(0);
        }
    }
}