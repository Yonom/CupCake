using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class SilverCrownReceiveMessage : ReceiveMessage
    {
        public SilverCrownReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; private set; }
    }
}