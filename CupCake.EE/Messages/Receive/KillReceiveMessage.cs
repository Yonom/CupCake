using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class KillReceiveMessage : ReceiveMessage
    {
        public int UserId { get; private set; }

        public KillReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}