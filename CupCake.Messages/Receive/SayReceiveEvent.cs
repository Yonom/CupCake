using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class SayReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        public SayReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Text = message.GetString(1);
            this.IsMyFriend = message.GetBoolean(2);
        }

        public bool IsMyFriend { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
    }
}