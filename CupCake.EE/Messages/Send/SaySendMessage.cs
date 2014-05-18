using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class SaySendMessage : SendMessage
    {
        public readonly string Text;

        public SaySendMessage(string text)
        {
            this.Text = text;
        }

        internal override Message GetMessage()
        {
            return Message.Create("say", this.Text);
        }
    }
}