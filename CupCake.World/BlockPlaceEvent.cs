using CupCake.Core.Events;
using CupCake.Messages.Blocks;

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