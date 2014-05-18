using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class GiveWitchReceiveMessage : ReceiveMessage
    {
        //No arguments

        internal GiveWitchReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}