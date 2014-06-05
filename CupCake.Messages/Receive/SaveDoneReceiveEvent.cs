using PlayerIOClient;

namespace CupCake.Messages.Receive
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