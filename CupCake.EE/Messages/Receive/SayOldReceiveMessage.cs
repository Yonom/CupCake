using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class SayOldReceiveMessage : ReceiveMessage
    {
        public bool IsMyFriend { get; private set; }
        public string Text { get; private set; }
        public string Username { get; private set; }

        public SayOldReceiveMessage(Message message)
            : base(message)
        {
            this.Username = message.GetString(0);
            this.Text = message.GetString(1);
            this.IsMyFriend = message.GetBoolean(2);
        }
    }
}