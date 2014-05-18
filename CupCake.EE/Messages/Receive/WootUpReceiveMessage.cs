using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class WootUpReceiveMessage : ReceiveMessage
    {
        //0

        public readonly int UserID;

        internal WootUpReceiveMessage(Message message)
            : base(message)
        {
            this.UserID = message.GetInteger(0);
        }
    }
}