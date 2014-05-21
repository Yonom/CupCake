using CupCake.EE.User;
using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class LevelUpReceiveEvent : ReceiveEvent, IUserEvent
    {
        public LevelUpReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.NewClass = (MagicClass)message.GetInteger(1);
        }

        public MagicClass NewClass { get; private set; }
        public int UserId { get; private set; }
    }
}