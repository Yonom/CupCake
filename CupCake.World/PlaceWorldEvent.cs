using CupCake.Core.Events;

namespace CupCake.World
{
    public class PlaceWorldEvent : Event
    {
        public PlaceWorldEvent(WorldBlock block)
        {
            this.Block = block;
        }

        public WorldBlock Block { get; set; }
    }
}