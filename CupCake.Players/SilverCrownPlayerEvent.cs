using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class SilverCrownPlayerEvent : PlayerEvent<SilverCrownReceiveEvent>
    {
        internal SilverCrownPlayerEvent(Player oldPlayer, Player player, SilverCrownReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}