using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class AccessReceiveEvent : ReceiveEvent
    {
        //No arguments

        public AccessReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}