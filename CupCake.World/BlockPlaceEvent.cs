using CupCake.Core.Events;

namespace CupCake.World
{
    public class BlockPlaceEvent : Event
    {
        public BlockPlaceEvent(WorldBlock block)
        {
            this.Block = block;
        }

        public WorldBlock Block { get; set; }
    }
}