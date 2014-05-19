using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class ResetReceiveMessage : ReceiveMessage
    {
        //No arguments

        public ResetReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}