using CupCake.Messages.Blocks;

namespace CupCake.World
{
    /// <summary>
    ///     Represents the types a <see cref="Block" /> can be.
    /// </summary>
    public enum BlockType
    {
        /// <summary>
        ///     A normal block
        /// </summary>
        Normal,

        /// <summary>
        ///     A rotatable block
        /// </summary>
        Rotatable,

        /// <summary>
        ///     A coin door block
        /// </summary>
        CoinDoor,

        /// <summary>
        ///     A portal block
        /// </summary>
        Portal,

        /// <summary>
        ///     A sound block
        /// </summary>
        Sound,

        /// <summary>
        ///     A label block
        /// </summary>
        Label,

        /// <summary>
        ///     A world portal block
        /// </summary>
        WorldPortal
    }
}