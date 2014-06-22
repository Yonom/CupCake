using CupCake.Core.Events;
using CupCake.Players;

namespace CupCake.World
{
    public class PlaceWorldEvent : Event
    {
        internal PlaceWorldEvent(WorldBlock worldBlock, Player player)
        {
            this.WorldBlock = worldBlock;
            this.Player = player;
        }

        public WorldBlock WorldBlock { get; private set; }
        public Player Player { get; private set; }
    }
}