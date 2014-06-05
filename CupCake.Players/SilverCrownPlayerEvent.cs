using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class SilverCrownPlayerEvent : PlayerEvent<SilverCrownReceiveEvent>
    {
        public SilverCrownPlayerEvent(Player player, SilverCrownReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}