using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class SilverCrownReceiveMessage : ReceiveMessage
    {
        public readonly int UserId;

        internal SilverCrownReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}