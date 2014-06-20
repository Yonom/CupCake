using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class ModModePlayerEvent : PlayerEvent<ModModeReceiveEvent>
    {
        internal ModModePlayerEvent(Player player, ModModeReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}