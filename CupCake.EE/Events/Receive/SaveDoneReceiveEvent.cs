using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class SaveDoneReceiveEvent : ReceiveEvent
    {
        //No arguments

        public SaveDoneReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}