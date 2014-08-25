using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class GodModePlayerEvent : PlayerEvent<GodModeReceiveEvent>
    {
        internal GodModePlayerEvent(Player oldPlayer, Player player, GodModeReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}