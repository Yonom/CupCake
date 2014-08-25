using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class PotionPlayerEvent : PlayerEvent<PotionReceiveEvent>
    {
        internal PotionPlayerEvent(Player oldPlayer, Player player, PotionReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}