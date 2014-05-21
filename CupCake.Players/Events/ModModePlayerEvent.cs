using CupCake.EE.Events.Receive;

namespace CupCake.Players.Events
{
    public class ModModePlayerEvent : PlayerEvent<ModModeReceiveEvent>
    {
        public ModModePlayerEvent(Player player, ModModeReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}