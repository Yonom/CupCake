using System.ComponentModel;
using CupCake.Core.Events;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when an EE message is recieved.
    /// </summary>
    public abstract class ReceiveEvent : Event
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        protected ReceiveEvent(Message message)
        {
            this.PlayerIOMessage = message;
        }

        /// <summary>
        ///     Gets the player io message.
        /// </summary>
        /// <value>The player io message.</value>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Message PlayerIOMessage { get; private set; }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return this.GetType().Name + this.PlayerIOMessage;
        }
    }
}