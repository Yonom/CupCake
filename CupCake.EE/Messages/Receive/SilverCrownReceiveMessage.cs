using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class SilverCrownReceiveMessage : ReceiveMessage
    {
        public int UserId { get; private set; }

        public SilverCrownReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}