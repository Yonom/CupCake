using System.Collections.Generic;
using CupCake.Core;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Occurs when everybody is teleported to the same location.
    /// </summary>
    public class TeleportEveryoneReceiveEvent : ReceiveEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public TeleportEveryoneReceiveEvent(Message message)
            : base(message)
        {
            var coords = new Dictionary<int, Point>();
            this.Coordinates = new ReadOnlyDictionary<int, Point>(coords);

            this.ResetCoins = message.GetBoolean(0);

            for (uint i = 1; i <= message.Count - 1u; i += 3)
            {
                coords.Add(message.GetInteger(i),
                    new Point(message.GetInteger(i + 1u), message.GetInteger(i + 2u)));
            }
        }

        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        /// <value>The coordinates.</value>
        public ReadOnlyDictionary<int, Point> Coordinates { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the coins need to be reset.
        /// </summary>
        /// <value><c>true</c> if the coins need to be reset; otherwise, <c>false</c>.</value>
        public bool ResetCoins { get; set; }
    }
}