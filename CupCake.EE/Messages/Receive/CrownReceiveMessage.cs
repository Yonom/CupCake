using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class CrownReceiveMessage : ReceiveMessage
    {
        public readonly int UserId;

        internal CrownReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}