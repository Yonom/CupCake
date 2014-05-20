using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class WriteReceiveEvent : ReceiveEvent
    {
        public WriteReceiveEvent(Message message)
            : base(message)
        {
            this.Title = message.GetString(0);
            this.Text = message.GetString(1);
        }

        public string Text { get; private set; }
        public string Title { get; private set; }
    }
}