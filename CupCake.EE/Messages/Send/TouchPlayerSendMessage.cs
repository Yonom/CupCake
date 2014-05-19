using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public class TouchPlayerSendMessage : SendMessage
    {
        public Potion Reason { get; set; }
        public int UserId { get; set; }

        public TouchPlayerSendMessage(int userId, Potion reason)
        {
            this.UserId = userId;
            this.Reason = reason;
        }

        public override Message GetMessage()
        {
            return Message.Create("touch", this.UserId, this.Reason);
        }
    }
}