using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class TouchUserSendEvent : SendEvent
    {
        public TouchUserSendEvent(int userId, Potion reason)
        {
            UserId = userId;
            Reason = reason;
        }

        public Potion Reason { get; set; }
        public int UserId { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("touch", UserId, Reason);
        }
    }
}