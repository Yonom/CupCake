using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class SayOldReceiveMessage : ReceiveMessage
    {
        //0
        //2

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