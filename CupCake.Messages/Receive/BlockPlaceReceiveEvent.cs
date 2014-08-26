using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    /// Class Block Place Receive Event.
    /// </summary>
    public class BlockPlaceReceiveEvent : ReceiveEvent, IBlockPlaceReceiveEvent, IUserReceiveEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlockPlaceReceiveEvent"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public BlockPlaceReceiveEvent(Message message)
            : base(message)
        {
            this.Layer = (Layer)message.GetInteger(0);
            this.PosX = message.GetInteger(1);
            this.PosY = message.GetInteger(2);
            this.Block = (Block)message.GetInteger(3);

            if (message.Count >= 5)
            {
                this.UserId = message.GetInteger(4);
            }
            else
            {
                this.UserId = -1;
            }
        }

        /// <summary>
        /// Gets or sets the block.
        /// </summary>
        /// <value>The block.</value>
        public Block Block { get; set; }
        /// <summary>
        /// Gets or sets the layer.
        /// </summary>
        /// <value>The layer.</value>
        public Layer Layer { get; set; }
        /// <summary>
        /// Gets or sets the position x of the player.
        /// </summary>
        /// <value>The position x.</value>
        public int PosX { get; set; }
        /// <summary>
        /// Gets or sets the position y of the player.
        /// </summary>
        /// <value>The position y.</value>
        public int PosY { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
    }
}