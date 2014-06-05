using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class LostAccessReceiveEvent : ReceiveEvent
    {
        //No arguments

        public LostAccessReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}