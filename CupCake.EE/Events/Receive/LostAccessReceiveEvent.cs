using PlayerIOClient;

namespace CupCake.EE.Events.Receive
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