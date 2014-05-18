using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class GiveFireWizardReceiveMessage : ReceiveMessage
    {
        //No arguments

        internal GiveFireWizardReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}