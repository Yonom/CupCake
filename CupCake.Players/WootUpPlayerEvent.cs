using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class WootUpPlayerEvent : PlayerEvent<WootUpReceiveEvent>
    {
        public WootUpPlayerEvent(Player player, WootUpReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}