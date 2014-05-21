using PlayerIOClient;

namespace CupCake.EE.Events.Receive
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