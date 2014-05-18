using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class SayReceiveMessage : ReceiveMessage
    {
        public readonly bool IsMyFriend;
        public readonly string Text;
        public readonly int UserId;

        internal SayReceiveMessage(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Text = message.GetString(1);
            this.IsMyFriend = message.GetBoolean(2);
        }
    }
}