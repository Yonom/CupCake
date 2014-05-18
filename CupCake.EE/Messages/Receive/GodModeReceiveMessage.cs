using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class GodModeReceiveMessage : ReceiveMessage
    {
        //0
        //1

        public readonly bool IsGod;
        public readonly int UserID;

        internal GodModeReceiveMessage(Message message)
            : base(message)
        {
            this.UserID = message.GetInteger(0);
            this.IsGod = message.GetBoolean(1);
        }
    }
}