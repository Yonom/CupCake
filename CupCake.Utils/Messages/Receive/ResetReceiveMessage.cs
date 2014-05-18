using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class ResetReceiveMessage : ReceiveMessage
    {
        //No arguments

        internal ResetReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}