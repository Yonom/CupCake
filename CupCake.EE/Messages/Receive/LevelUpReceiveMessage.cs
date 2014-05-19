using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class LevelUpReceiveMessage : ReceiveMessage
    {
        public int NewClass { get; private set; }
        public int UserId { get; private set; }

        public LevelUpReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.NewClass = message.GetInteger(1);
        }
    }
}