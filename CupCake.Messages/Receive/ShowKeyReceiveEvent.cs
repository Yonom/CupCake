using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Occurs when the keys are visible.
    /// </summary>
    public class ShowKeyReceiveEvent : ReceiveEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ShowKeyReceiveEvent(Message message)
            : base(message)
        {
            this.Keys = new Key[message.Count];
            for (uint i = 0; i <= message.Count - 1u; i++)
            {
                this.Keys[(int)i] = (Key)Enum.Parse(typeof(Key), message.GetString(i), true);
            }
        }

        /// <summary>
        /// Gets or sets the keys.
        /// </summary>
        /// <value>The keys.</value>
        public Key[] Keys { get; set; }
    }
}