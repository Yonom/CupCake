using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class WriteReceiveMessage : ReceiveMessage
    {
        public readonly string Text;
        public readonly string Title;

        internal WriteReceiveMessage(Message message)
            : base(message)
        {
            this.Title = message.GetString(0);
            this.Text = message.GetString(1);
        }
    }
}