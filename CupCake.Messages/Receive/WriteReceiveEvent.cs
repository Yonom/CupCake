using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class WriteReceiveEvent : ReceiveEvent
    {
        public WriteReceiveEvent(Message message)
            : base(message)
        {
            this.Title = message.GetString(0);
            this.Text = message.GetString(1);
        }

        public string Text { get; set; }
        public string Title { get; set; }
    }
}