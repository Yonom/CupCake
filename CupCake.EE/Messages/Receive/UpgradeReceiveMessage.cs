using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class UpgradeReceiveMessage : ReceiveMessage
    {
        //No arguments

        public UpgradeReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}