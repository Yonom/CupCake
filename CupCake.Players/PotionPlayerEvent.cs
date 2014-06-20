using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class PotionPlayerEvent : PlayerEvent<PotionReceiveEvent>
    {
        internal PotionPlayerEvent(Player player, PotionReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}