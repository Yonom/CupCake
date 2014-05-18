using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public class MagicReceiveMessage : ReceiveMessage
    {
        public readonly int UserId;

        internal MagicReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}