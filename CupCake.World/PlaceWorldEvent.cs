using CupCake.Core.Events;
using CupCake.Players;

namespace CupCake.World
{
    /// <summary>
    ///     Occurs when a block is placed.
    /// </summary>
    public class PlaceWorldEvent : Event
    {
        internal PlaceWorldEvent(WorldBlock worldBlock, WorldBlock oldWorldBlock, Player player)
        {
            this.OldWorldBlock = oldWorldBlock;
            this.WorldBlock = worldBlock;
            this.Player = player;
        }

        /// <summary>
        ///     Gets the world block.
        /// </summary>
        /// <value>
        ///     The world block.
        /// </value>
        public WorldBlock WorldBlock { get; private set; }

        /// <summary>
        ///     Gets the old world block.
        /// </summary>
        /// <value>
        ///     The old world block.
        /// </value>
        public WorldBlock OldWorldBlock { get; private set; }

        /// <summary>
        ///     Gets the player that placed this block.
        /// </summary>
        /// <value>
        ///     The player that placed this block.
        /// </value>
        public Player Player { get; private set; }
    }
}