using CupCake.Core.Events;
using CupCake.Players;

namespace CupCake.World
{
    public class PlaceWorldEvent : Event
    {
        internal PlaceWorldEvent(WorldBlock block, Player player)
        {
            this.Block = block;
            this.Player = player;
        }

        public WorldBlock Block { get; private set; }
        public Player Player { get; private set; }
    }
}