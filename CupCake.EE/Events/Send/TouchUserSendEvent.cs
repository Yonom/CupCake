using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public class TouchUserSendEvent : SendEvent, IUserEvent
    {
        public TouchUserSendEvent(int userId, Potion reason)
        {
            this.UserId = userId;
            this.Reason = reason;
        }

        public Potion Reason { get; set; }
        public int UserId { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("touch", this.UserId, this.Reason);
        }
    }
}