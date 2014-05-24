using CupCake.Messages.Events.Receive;

namespace CupCake.Players.Events
{
    public class PotionPlayerEvent : PlayerEvent<PotionReceiveEvent>
    {
        public PotionPlayerEvent(Player player, PotionReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}