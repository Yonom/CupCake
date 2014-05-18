using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class GiveDarkWizardReceiveMessage : ReceiveMessage
    {
        //No arguments

        internal GiveDarkWizardReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}