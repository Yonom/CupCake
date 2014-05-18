using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class GiveWizardReceiveMessage : ReceiveMessage
    {
        //No arguments

        internal GiveWizardReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}