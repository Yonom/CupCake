using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public class WootUpReceiveMessage : ReceiveMessage
    {
        public readonly int UserId;

        internal WootUpReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}