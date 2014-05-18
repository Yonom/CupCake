using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class UpgradeReceiveMessage : ReceiveMessage
    {
        //No arguments

        internal UpgradeReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}