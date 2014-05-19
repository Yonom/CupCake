using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class SilverCrownReceiveMessage : ReceiveMessage
    {
        public readonly int UserId;

        public SilverCrownReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}