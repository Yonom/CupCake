using PlayerIOClient;

namespace CupCake.Messages.Receive
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