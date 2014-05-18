using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
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