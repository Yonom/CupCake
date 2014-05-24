using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
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