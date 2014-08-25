using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class GuardianModePlayerEvent : PlayerEvent<GuardianModeReceiveEvent>
    {
        internal GuardianModePlayerEvent(Player oldPlayer, Player player, GuardianModeReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}