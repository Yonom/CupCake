using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class ShowPurpleSendEvent : SendEvent
    {
        public bool Show { get; set; }

        public ShowPurpleSendEvent(bool show)
        {
            this.Show = show;
        }

        public override Message GetMessage()
        {
            return Message.Create("sp", this.Show);
        }
    }
}