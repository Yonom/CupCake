using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
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