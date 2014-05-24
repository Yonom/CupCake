using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
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