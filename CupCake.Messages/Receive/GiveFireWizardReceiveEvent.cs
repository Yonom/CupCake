using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Class Give Fire Wizard Receive Event.
    /// </summary>
    public class GiveFireWizardReceiveEvent : ReceiveEvent
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GiveFireWizardReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The EE message.</param>
        public GiveFireWizardReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}