using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    /// Class GodMode Send Event
    /// </summary>
    public class GodModeSendEvent : SendEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GodModeSendEvent"/> class.
        /// </summary>
        /// <param name="godModeEnabled">if set to <c>true</c> then god mode enabled.</param>
        public GodModeSendEvent(bool godModeEnabled)
        {
            this.GodModeEnabled = godModeEnabled;
        }

        /// <summary>
        /// Gets or sets a value indicating whether god mode is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if god mode is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool GodModeEnabled { get; set; }

        /// <summary>
        /// Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("god", this.GodModeEnabled);
        }
    }
}