using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class SaySendMessage : SendMessage
    {
        public SaySendMessage(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("say", this.Text);
        }
    }
}