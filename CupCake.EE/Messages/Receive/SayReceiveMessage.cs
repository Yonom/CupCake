using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class SayReceiveMessage : ReceiveMessage
    {
        //0
        //2

        public readonly bool IsMyFriend;
        public readonly string Text;
        public readonly int UserID;

        internal SayReceiveMessage(Message message)
            : base(message)
        {
            this.UserID = message.GetInteger(0);
            this.Text = message.GetString(1);
            this.IsMyFriend = message.GetBoolean(2);
        }
    }
}