using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public sealed class SaySendEvent : SendEvent
    {
        public SaySendEvent(string text)
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