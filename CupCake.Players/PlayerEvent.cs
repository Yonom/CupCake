using CupCake.Core.Events;
using CupCake.Messages;

namespace CupCake.Players
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