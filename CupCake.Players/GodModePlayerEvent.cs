using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class GodModePlayerEvent : PlayerEvent<GodModeReceiveEvent>
    {
        internal GodModePlayerEvent(Player player, GodModeReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}