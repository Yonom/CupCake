using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class GodModePlayerEvent : PlayerEvent<GodModeReceiveEvent>
    {
        public GodModePlayerEvent(Player player, GodModeReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}