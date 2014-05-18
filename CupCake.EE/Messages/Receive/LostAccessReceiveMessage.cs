using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class LostAccessReceiveMessage : ReceiveMessage
    {
        //No arguments

        internal LostAccessReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}