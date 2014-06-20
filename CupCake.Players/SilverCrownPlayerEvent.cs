using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class SilverCrownPlayerEvent : PlayerEvent<SilverCrownReceiveEvent>
    {
        internal SilverCrownPlayerEvent(Player player, SilverCrownReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}