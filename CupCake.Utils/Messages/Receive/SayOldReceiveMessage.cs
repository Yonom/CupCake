using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class SayOldReceiveMessage : ReceiveMessage
    {
        public readonly bool IsMyFriend;
        public readonly string Text;
        public readonly string Username;

        internal SayOldReceiveMessage(Message message)
            : base(message)
        {
            this.Username = message.GetString(0);
            this.Text = message.GetString(1);
            this.IsMyFriend = message.GetBoolean(2);
        }
    }
}