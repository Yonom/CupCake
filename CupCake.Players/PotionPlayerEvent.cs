using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class PotionPlayerEvent : PlayerEvent<PotionReceiveEvent>
    {
        public PotionPlayerEvent(Player player, PotionReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}