using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class LeftReceiveMessage : ReceiveMessage
    {
        public int UserId { get; private set; }

        public LeftReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}