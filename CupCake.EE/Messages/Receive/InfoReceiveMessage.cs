using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class InfoReceiveMessage : ReceiveMessage
    {
        public readonly string Text;
        public readonly string Title;

        public InfoReceiveMessage(Message message)
            : base(message)
        {
            this.Title = message.GetString(0);
            this.Text = message.GetString(1);
        }
    }
}