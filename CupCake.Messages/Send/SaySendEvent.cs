using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Class Say Send Event
    /// </summary>
    public class SaySendEvent : SendEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SaySendEvent" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public SaySendEvent(string text)
        {
            this.Text = text;
        }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        ///     Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("say", this.Text);
        }
    }
}