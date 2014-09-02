using CupCake.Messages.Blocks;

namespace CupCake.Messages.Send
{
    /// <summary>
    /// Interface Block Place Send Event
    /// </summary>
    public interface IBlockPlaceSendEvent : IEncryptedSendEvent
    {
        /// <summary>
        /// Gets or sets the layer.
        /// </summary>
        /// <value>
        /// The layer.
        /// </value>
        Layer Layer { get; set; }

        /// <summary>
        /// Gets or sets the x-coordinate.
        /// </summary>
        /// <value>
        /// The x-coordinate.
        /// </value>
        int X { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate.
        /// </summary>
        /// <value>
        /// The y-coordinate.
        /// </value>
        int Y { get; set; }

        /// <summary>
        /// Gets or sets the block.
        /// </summary>
        /// <value>
        /// The block.
        /// </value>
        Block Block { get; set; }
    }
}