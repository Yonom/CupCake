using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
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