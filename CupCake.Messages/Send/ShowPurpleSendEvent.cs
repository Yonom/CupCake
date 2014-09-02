using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    /// Class Show Purple Send Event
    /// </summary>
    public class ShowPurpleSendEvent : SendEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowPurpleSendEvent"/> class.
        /// </summary>
        /// <param name="show">whether the switch is enabled (purple gates are solid)</param>
        public ShowPurpleSendEvent(bool show)
        {
            this.Show = show;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this switch is enabled (purple gates are solid).
        /// </summary>
        /// <value>
        ///   <c>true</c> if the switch is enabled (purple gates are solid); otherwise, <c>false</c>.
        /// </value>
        public bool Show { get; set; }

        /// <summary>
        /// Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("sp", this.Show);
        }
    }
}