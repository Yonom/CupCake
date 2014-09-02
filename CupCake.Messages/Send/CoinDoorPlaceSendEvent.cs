using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    /// <summary>
    /// Class CoinDoor Place Send Event
    /// </summary>
    public class CoinDoorPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoinDoorPlaceSendEvent"/> class.
        /// </summary>
        /// <param name="layer">The layer.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="block">The block.</param>
        /// <param name="coinsToCollect">The number of coins to collect.</param>
        public CoinDoorPlaceSendEvent(Layer layer, int x, int y, CoinDoorBlock block, uint coinsToCollect)
        {
            this.Block = block;
            this.X = x;
            this.Y = y;
            this.Layer = BlockUtils.CorrectLayer((Block)block, layer);

            this.CoinsToCollect = coinsToCollect;
        }

        /// <summary>
        /// Gets or sets the block.
        /// </summary>
        /// <value>
        /// The block.
        /// </value>
        public CoinDoorBlock Block { get; set; }

        /// <summary>
        /// Gets or sets the number of coins to collect.
        /// </summary>
        /// <value>
        /// The number of coins to collect.
        /// </value>
        public uint CoinsToCollect { get; set; }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block)this.Block; }
            set { this.Block = (CoinDoorBlock)value; }
        }

        /// <summary>
        /// Gets or sets the layer.
        /// </summary>
        /// <value>
        /// The layer.
        /// </value>
        public Layer Layer { get; set; }

        /// <summary>
        /// Gets or sets the x-coordinate.
        /// </summary>
        /// <value>
        /// The x-coordinate.
        /// </value>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate.
        /// </summary>
        /// <value>
        /// The y-coordinate.
        /// </value>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the encryption string.
        /// </summary>
        /// <value>
        /// The encryption string.
        /// </value>
        public string Encryption { get; set; }

        /// <summary>
        /// Gets the PlayerIO message representing the data in this <see cref="SendEvent" />.
        /// </summary>
        /// <returns></returns>
        public override Message GetMessage()
        {
            return Message.Create(this.Encryption, (int)this.Layer, this.X, this.Y, (int)this.Block, this.CoinsToCollect);
        }
    }
}