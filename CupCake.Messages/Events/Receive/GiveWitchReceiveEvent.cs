using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class GiveWitchReceiveEvent : ReceiveEvent
    {
        //No arguments

        public GiveWitchReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}