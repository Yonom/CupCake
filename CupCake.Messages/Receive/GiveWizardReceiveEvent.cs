using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Class Give Wizard Receive Event.
    /// </summary>
    public class GiveWizardReceiveEvent : ReceiveEvent
    {
        //No arguments

        /// <summary>
        /// Initializes a new instance of the <see cref="GiveWizardReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The EE message.</param>
        public GiveWizardReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}