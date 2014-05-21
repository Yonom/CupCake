using PlayerIOClient;

namespace CupCake.EE.Events.Receive
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