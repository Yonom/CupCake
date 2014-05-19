using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class WriteReceiveMessage : ReceiveMessage
    {
        public string Text { get; private set; }
        public string Title { get; private set; }

        public WriteReceiveMessage(Message message)
            : base(message)
        {
            this.Title = message.GetString(0);
            this.Text = message.GetString(1);
        }
    }
}