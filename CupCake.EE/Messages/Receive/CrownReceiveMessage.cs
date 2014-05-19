using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class CrownReceiveMessage : ReceiveMessage
    {
        public int UserId { get; private set; }

        public CrownReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}