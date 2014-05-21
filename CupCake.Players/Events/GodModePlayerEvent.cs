using CupCake.EE.Events.Receive;

namespace CupCake.Players.Events
{
    public class GodModePlayerEvent : PlayerEvent<GodModeReceiveEvent>
    {
        public GodModePlayerEvent(Player player, GodModeReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}