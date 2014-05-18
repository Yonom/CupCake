using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class SilverCrownReceiveMessage : ReceiveMessage
    {
        //0

        public readonly int UserID;

        internal SilverCrownReceiveMessage(Message message)
            : base(message)
        {
            this.UserID = message.GetInteger(0);
        }
    }
}