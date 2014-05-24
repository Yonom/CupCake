using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
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