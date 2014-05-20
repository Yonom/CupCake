using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class GiveFireWizardReceiveEvent : ReceiveEvent
    {
        //No arguments

        public GiveFireWizardReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}