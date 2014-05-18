using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public class KillReceiveMessage : ReceiveMessage
    {
        public readonly int UserId;

        internal KillReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}