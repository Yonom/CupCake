using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class UpgradeReceiveEvent : ReceiveEvent
    {
        //No arguments

        public UpgradeReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}