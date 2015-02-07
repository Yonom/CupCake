using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when a key becomes visible. (not pressed state)
    /// </summary>
    public class ShowKeyReceiveEvent : ReceiveEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ShowKeyReceiveEvent(Message message)
            : base(message)
        {
            this.Keys = new Key[message.Count];
            for (uint i = 0; i <= message.Count - 1u; i++)
            {
                this.Keys[(int)i] = (Key)message.GetInt(i);
            }
        }

        /// <summary>
        ///     Gets or sets the keys.
        /// </summary>
        /// <value>The keys.</value>
        public Key[] Keys { get; set; }
    }
}