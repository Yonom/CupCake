using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class SaveDoneReceiveMessage : ReceiveMessage
    {
        //No arguments

        internal SaveDoneReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}