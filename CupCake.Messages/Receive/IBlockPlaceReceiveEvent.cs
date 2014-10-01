using CupCake.Messages.Blocks;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Interface IBlockPlaceReceiveEvent
    /// </summary>
    public interface IBlockPlaceReceiveEvent
    {
        /// <summary>
        ///     Gets or sets the layer.
        /// </summary>
        /// <value>The layer.</value>
        Layer Layer { get; set; }

        /// <summary>
        ///     Gets or sets the position x.
        /// </summary>
        /// <value>The position x.</value>
        int PosX { get; set; }

        /// <summary>
        ///     Gets or sets the position y.
        /// </summary>
        /// <value>The position y.</value>
        int PosY { get; set; }

        /// <summary>
        ///     Gets or sets the block.
        /// </summary>
        /// <value>The block.</value>
        Block Block { get; set; }
    }
}