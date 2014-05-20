using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class GiveGrinchReceiveEvent : ReceiveEvent
    {
        //No arguments

        public GiveGrinchReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}