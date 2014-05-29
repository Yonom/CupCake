using CupCake.Core.Events;
using CupCake.Messages.Events;

namespace CupCake.Players.Events
{
    public abstract class PlayerEvent<TBase> : Event where TBase : Event, IUserEvent
    {
        protected PlayerEvent(Player player, TBase innerEvent)
        {
            this.Player = player;
            this.InnerEvent = innerEvent;
        }

        public TBase InnerEvent { get; set; }
        public Player Player { get; set; }
    }
}