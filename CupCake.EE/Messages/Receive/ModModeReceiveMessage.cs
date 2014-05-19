using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class ModModeReceiveMessage : ReceiveMessage
    {
        public int UserId { get; private set; }

        public ModModeReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
        }
    }
}