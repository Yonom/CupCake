using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class SaveDoneReceiveEvent : ReceiveEvent
    {
        //No arguments

        public SaveDoneReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}