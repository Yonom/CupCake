using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class LeftReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        public LeftReceiveEvent(Message message)
            : base(message)
        {
            UserId = message.GetInteger(0);
        }

        public int UserId { get; set; }
    }
}