using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class GiveDarkWizardReceiveEvent : ReceiveEvent
    {
        //No arguments

        public GiveDarkWizardReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}