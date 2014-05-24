using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
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