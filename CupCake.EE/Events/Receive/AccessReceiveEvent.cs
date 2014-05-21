using PlayerIOClient;

namespace CupCake.EE.Events.Receive
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