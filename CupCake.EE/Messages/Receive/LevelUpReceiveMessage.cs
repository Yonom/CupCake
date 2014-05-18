using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public class LevelUpReceiveMessage : ReceiveMessage
    {
        public readonly int NewClass;
        public readonly int UserID;

        internal LevelUpReceiveMessage(Message message)
            : base(message)
        {
            this.UserID = message.GetInteger(0);
            this.NewClass = message.GetInteger(1);
        }
    }
}