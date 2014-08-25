using CupCake.Core.Events;

namespace CupCake.Players
{
    public abstract class PlayerEvent<TBase> : Event
    {
        internal PlayerEvent(Player oldPlayer, Player player, TBase innerEvent)
        {
            this.Player = player;
            this.InnerEvent = innerEvent;
        }

        public TBase InnerEvent { get; private set; }
        public Player OldPlayer { get; private set; }
        public Player Player { get; private set; }
    }
}