using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class CrownReceiveMessage : ReceiveMessage
    {
        public readonly int UserID;

        internal CrownReceiveMessage(Message message)
            : base(message)
        {
            this.UserID = message.GetInteger(0);
        }
    }
}