using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class SayReceiveMessage : ReceiveMessage
    {
        public bool IsMyFriend { get; private set; }
        public string Text { get; private set; }
        public int UserId { get; private set; }

        public SayReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Text = message.GetString(1);
            this.IsMyFriend = message.GetBoolean(2);
        }
    }
}