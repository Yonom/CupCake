using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class KillReceiveMessage : ReceiveMessage
    {
        public readonly int UserID;

        internal KillReceiveMessage(Message message)
            : base(message)
        {
            this.UserID = message.GetInteger(0);
        }
    }
}