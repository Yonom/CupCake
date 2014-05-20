using CupCake.EE.Players;
using PlayerIOClient;

namespace CupCake.EE.Events.Send
{
    public sealed class AutoSaySendEvent : SendEvent
    {
        public AutoSaySendEvent(AutoText text)
        {
            this.Text = text;
        }

        public AutoText Text { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("autosay", this.Text);
        }
    }
}