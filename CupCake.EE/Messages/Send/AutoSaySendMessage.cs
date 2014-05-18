using CupCake.EE.Players;
using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class AutoSaySendMessage : SendMessage
    {
        public readonly AutoText Text;

        public AutoSaySendMessage(AutoText text)
        {
            this.Text = text;
        }

        public override Message GetMessage()
        {
            return Message.Create("autosay", this.Text);
        }
    }
}