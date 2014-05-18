using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class LevelUpReceiveMessage : ReceiveMessage
    {
        public readonly int NewClass;
        public readonly int UserId;

        internal LevelUpReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.NewClass = message.GetInteger(1);
        }
    }
}