using CupCake.Core.Events;
using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public abstract class PlayerEvent<TBase> : Event where TBase : IUserReceiveEvent
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