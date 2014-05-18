using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public class TouchPlayerSendMessage : SendMessage
    {
        public readonly Potion Reason;
        public readonly int UserId;

        public TouchPlayerSendMessage(int userId, Potion reason)
        {
            this.UserId = userId;
            this.Reason = reason;
        }

        internal override Message GetMessage()
        {
            return Message.Create("touch", this.UserId, this.Reason);
        }
    }
}