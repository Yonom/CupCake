using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class InfoReceiveEvent : ReceiveEvent
    {
        public InfoReceiveEvent(Message message)
            : base(message)
        {
            this.Title = message.GetString(0);
            this.Text = message.GetString(1);
        }

        public string Text { get; set; }
        public string Title { get; set; }
    }
}