using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class ShowPurpleSendEvent : SendEvent
    {
        public ShowPurpleSendEvent(bool show)
        {
            this.Show = show;
        }

        public bool Show { get; set; }

        public override Message GetMessage()
        {
            return Message.Create("sp", this.Show);
        }
    }
}