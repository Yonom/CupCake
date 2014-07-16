using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class SayReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        public SayReceiveEvent(Message message)
            : base(message)
        {
            UserId = message.GetInteger(0);
            Text = message.GetString(1);
            IsMyFriend = message.GetBoolean(2);
        }

        public bool IsMyFriend { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
    }
}