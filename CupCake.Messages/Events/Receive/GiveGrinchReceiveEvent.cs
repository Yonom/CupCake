using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class GiveGrinchReceiveEvent : ReceiveEvent
    {
        //No arguments

        public GiveGrinchReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}