using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class WootUpReceiveMessage : ReceiveMessage
    {
        public readonly int UserId;

        public WootUpReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}