using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Class Coin Receive Event.
    /// </summary>
    public class CoinReceiveEvent : ReceiveEvent, IUserReceiveEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoinReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CoinReceiveEvent(Message message)
            : base(message)
        {
            this.UserId = message.GetInteger(0);
            this.Coins = message.GetInteger(1);
            this.BlueCoins = message.GetInteger(2);
        }

        /// <summary>
        /// Gets or sets the coins of the player.
        /// </summary>
        /// <value>The coins.</value>
        public int Coins { get; set; }        
        /// <summary>
        /// Gets or sets the blue coins of the player.
        /// </summary>
        /// <value>The blue coins.</value>
        public int BlueCoins { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
    }
}