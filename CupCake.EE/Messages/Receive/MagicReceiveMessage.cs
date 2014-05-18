using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class MagicReceiveMessage : ReceiveMessage
    {
        public readonly int UserID;

        internal MagicReceiveMessage(Message message)
            : base(message)
        {
            this.UserID = message.GetInteger(0);
        }
    }
}