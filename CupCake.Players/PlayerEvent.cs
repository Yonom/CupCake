using CupCake.Core.Events;

namespace CupCake.Players
{
    public abstract class PlayerEvent<TBase> : Event
    {
        private readonly Player _oldPlayer;

        internal PlayerEvent(Player oldPlayer, Player player, TBase innerEvent)
        {
            this._oldPlayer = oldPlayer;
            this.Player = player;
            this.InnerEvent = innerEvent;
        }

        public TBase InnerEvent { get; private set; }

        public virtual Player OldPlayer
        {
            get { return this._oldPlayer; }
        }

        public Player Player { get; private set; }
    }
}