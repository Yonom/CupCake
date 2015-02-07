using CupCake.Messages.Blocks;

using PlayerIOClient;
using System;
using System.Collections.Generic;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when a key is hidden.
    /// </summary>
    public class HideKeyReceiveEvent : ReceiveEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HideKeyReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public HideKeyReceiveEvent(Message message)
            : base(message)
        {
            this.Keys = new List<KeyTrigger>();
            for (uint i = 0; i <= message.Count - 1u; i++)
            {
                if (message[i] is string)
                {
                    Key key = (Key)(Key)Enum.Parse(typeof(Key), message.GetString(i), true);
                    if (BlockUtils.IsKey(key))
                    {
                        i++;
                        int userId = message.GetInt(i);
                        Keys.Add(new KeyPress(key, userId));
                    }
                    else
                    {
                        Keys.Add(new KeyTrigger(key));
                    }
                }
            }
        }

        /// <summary>
        ///     Gets or sets the keys.
        /// </summary>
        /// <value>The keys.</value>
        public List<KeyTrigger> Keys { get; set; }
    }
}