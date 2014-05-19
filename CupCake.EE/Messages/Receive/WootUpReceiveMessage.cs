using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class WootUpReceiveMessage : ReceiveMessage
    {
        public WootUpReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }

        public int UserId { get; private set; }
    }
}