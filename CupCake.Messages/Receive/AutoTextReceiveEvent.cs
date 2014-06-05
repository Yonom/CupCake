using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class AutoTextReceiveEvent : ReceiveEvent, IUserEvent
    {
        public AutoTextReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.AutoText = message.GetString(1);
        }

        public string AutoText { get; set; }
        public int UserId { get; set; }
    }
}