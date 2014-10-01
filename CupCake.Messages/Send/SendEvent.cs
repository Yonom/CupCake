using CupCake.Core.Events;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Class Send Event
    /// </summary>
    public abstract class SendEvent : Event
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="SendEvent" /> is cancelled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if cancelled; otherwise, <c>false</c>.
        /// </value>
        public bool Cancelled { get; set; }

        /// <summary>
        ///     Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public abstract Message GetMessage();
    }
}