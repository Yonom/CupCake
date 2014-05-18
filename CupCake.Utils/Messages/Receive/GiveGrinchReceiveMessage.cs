using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class GiveGrinchReceiveMessage : ReceiveMessage
    {
        //No arguments

        internal GiveGrinchReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}