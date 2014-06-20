using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class WootUpPlayerEvent : PlayerEvent<WootUpReceiveEvent>
    {
        internal WootUpPlayerEvent(Player player, WootUpReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}