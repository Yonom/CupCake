using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class SaySendEvent : SendEvent
    {
        public SaySendEvent(string text)
        {
            Text = text;
        }

        public string Text { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("say", Text);
        }
    }
}