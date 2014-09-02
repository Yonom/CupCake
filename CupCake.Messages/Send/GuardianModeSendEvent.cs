using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    /// Class GuardianMode Send Event
    /// </summary>
    public class GuardianModeSendEvent : SendEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuardianModeSendEvent"/> class.
        /// </summary>
        /// <param name="guardianModeEnabled">if set to <c>true</c> then guardian mode enabled.</param>
        public GuardianModeSendEvent(bool guardianModeEnabled)
        {
            this.GuardianModeEnabled = guardianModeEnabled;
        }

        /// <summary>
        /// Gets or sets a value indicating whether guardian mode is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if guardian mode is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool GuardianModeEnabled { get; set; }

        /// <summary>
        /// Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("guardian", this.GuardianModeEnabled);
        }
    }
}