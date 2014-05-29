using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class SayOldReceiveEvent : ReceiveEvent
    {
        public SayOldReceiveEvent(Message message)
            : base(message)
        {
            this.Username = message.GetString(0);
            this.Text = message.GetString(1);
            this.IsMyFriend = message.GetBoolean(2);
        }

        public bool IsMyFriend { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
    }
}