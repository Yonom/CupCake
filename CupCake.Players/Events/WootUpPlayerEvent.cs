using CupCake.Messages.Events.Receive;

namespace CupCake.Players.Events
{
    public class WootUpPlayerEvent : PlayerEvent<WootUpReceiveEvent>
    {
        public WootUpPlayerEvent(Player player, WootUpReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}