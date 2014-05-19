using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class KillReceiveMessage : ReceiveMessage
    {
        public readonly int UserId;

        public KillReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}