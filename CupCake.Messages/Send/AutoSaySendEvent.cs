using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Class Auto Say Send Event
    /// </summary>
    public class AutoSaySendEvent : SendEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoSaySendEvent" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public AutoSaySendEvent(AutoText text)
        {
            this.Text = text;
        }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public AutoText Text { get; set; }

        /// <summary>
        ///     Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("autosay", (int)this.Text);
        }
    }
}