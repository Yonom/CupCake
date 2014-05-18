using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class AccessReceiveMessage : ReceiveMessage
    {
        //No arguments

        internal AccessReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}