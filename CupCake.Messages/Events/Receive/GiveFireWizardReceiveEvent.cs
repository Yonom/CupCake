using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class GiveFireWizardReceiveEvent : ReceiveEvent
    {
        //No arguments

        public GiveFireWizardReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}