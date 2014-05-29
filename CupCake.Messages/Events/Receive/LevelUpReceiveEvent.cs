using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class LevelUpReceiveEvent : ReceiveEvent, IUserEvent
    {
        public LevelUpReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.NewClass = (MagicClass)message.GetInteger(1);
        }

        public MagicClass NewClass { get; set; }
        public int UserId { get; set; }
    }
}