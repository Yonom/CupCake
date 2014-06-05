using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class ModModePlayerEvent : PlayerEvent<ModModeReceiveEvent>
    {
        public ModModePlayerEvent(Player player, ModModeReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}