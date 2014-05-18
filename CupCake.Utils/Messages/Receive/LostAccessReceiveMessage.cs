using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
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