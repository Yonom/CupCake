using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class ResetReceiveEvent : ReceiveEvent
    {
        //No arguments

        public ResetReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}