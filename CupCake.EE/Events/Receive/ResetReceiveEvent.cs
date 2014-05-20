using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class ResetReceiveEvent : ReceiveEvent
    {
        //No arguments

        public ResetReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}