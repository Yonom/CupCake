using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class AutoTextReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        public AutoTextReceiveEvent(Message message)
            : base(message)
        {
            UserId = message.GetInteger(0);
            AutoText = message.GetString(1);
        }

        public string AutoText { get; set; }
        public int UserId { get; set; }
    }
}