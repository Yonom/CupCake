using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class AutoTextReceiveMessage : ReceiveMessage
    {
        public AutoTextReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.AutoText = message.GetString(1);
        }

        public string AutoText { get; private set; }
        public int UserId { get; private set; }
    }
}