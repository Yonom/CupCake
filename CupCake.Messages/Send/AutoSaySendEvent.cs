using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class AutoSaySendEvent : SendEvent
    {
        public AutoSaySendEvent(AutoText text)
        {
            this.Text = text;
        }

        public AutoText Text { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("autosay", (int)this.Text);
        }
    }
}