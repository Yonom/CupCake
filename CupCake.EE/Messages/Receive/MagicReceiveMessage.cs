using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class MagicReceiveMessage : ReceiveMessage
    {
        public readonly int UserId;

        public MagicReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}