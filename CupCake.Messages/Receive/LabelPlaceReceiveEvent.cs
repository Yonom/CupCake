using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when a label is placed in the world.
    /// </summary>
    public class LabelPlaceReceiveEvent : SignPlaceReceiveEvent
    {
        public LabelPlaceReceiveEvent(Message message)
            : base(message)
        {
            this.TextColor = message.GetString(4);
        }

        /// <summary>
        ///     Gets or sets the hexadecimal text color.
        /// </summary>
        public string TextColor { get; set; }
    }
}