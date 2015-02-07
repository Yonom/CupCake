using System;
using System.Linq;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when a key is hidden.
    /// </summary>
    public class HideKeyReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HideKeyReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public HideKeyReceiveEvent(Message message)
            : base(message)
        {
            this.Keys = new Key[message.Count];
            for (uint i = 0; i <= message.Count - 2u; i++)
            {
                this.Keys[(int)i] = (Key)Enum.Parse(typeof(Key), message.GetString(i), true);
            }

            if (this.Keys.First() != Key.TimeDoor)
            {
                this.UserId = message.GetInt(message.Count - 1);
            }
        }

        /// <summary>
        ///     Gets or sets the keys.
        /// </summary>
        /// <value>The keys.</value>
        public Key[] Keys { get; set; }

        public int UserId { get; set; }
    }
}