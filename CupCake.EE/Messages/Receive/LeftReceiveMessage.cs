using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class LeftReceiveMessage : ReceiveMessage
    {
        public readonly int UserId;

        public LeftReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}