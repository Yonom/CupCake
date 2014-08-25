using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class WootUpPlayerEvent : PlayerEvent<WootUpReceiveEvent>
    {
        internal WootUpPlayerEvent(Player oldPlayer, Player player, WootUpReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}