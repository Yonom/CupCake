using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class UpgradeReceiveEvent : ReceiveEvent
    {
        //No arguments

        public UpgradeReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}