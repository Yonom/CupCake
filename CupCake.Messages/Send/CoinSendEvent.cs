using System;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    ///     Class Coin Send Event
    /// </summary>
    public class CoinSendEvent : SendEvent
    {
        public CoinSendEvent(int coins, int blueCoins, int coinX, int coinY)
        {
            this.Coins = coins;
            this.BlueCoins = blueCoins;
            this.CoinX = coinX;
            this.CoinY = coinY;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoinSendEvent" /> class.
        /// </summary>
        /// <param name="coins">The number of coins the player has.</param>
        /// <param name="coinX">The coin x-coordinate.</param>
        /// <param name="coinY">The coin y-coordinate.</param>
        [Obsolete("Use the other constructor with blue coins parameter", true)]
        public CoinSendEvent(int coins, int coinX, int coinY)
            : this(coins, 0, coinX, coinY)
        {
        }

        /// <summary>
        ///     Gets or sets the coin x-coordinate.
        /// </summary>
        /// <value>
        ///     The coin x-coordinate.
        /// </value>
        public int CoinX { get; set; }

        /// <summary>
        ///     Gets or sets the coin y-coordinate.
        /// </summary>
        /// <value>
        ///     The coin y-coordinate.
        /// </value>
        public int CoinY { get; set; }

        /// <summary>
        ///     Gets or sets the number of yellow coins the player has.
        /// </summary>
        /// <value>
        ///     The number of yellow coins the player has.
        /// </value>
        public int Coins { get; set; }

        /// <summary>
        ///     Gets or sets the number of blue coins the player has.
        /// </summary>
        /// <value>
        ///     The number of blue coins the player has.
        /// </value>
        public int BlueCoins { get; set; }

        /// <summary>
        ///     Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create("c", this.Coins, this.BlueCoins, this.CoinX, this.CoinY);
        }
    }
}