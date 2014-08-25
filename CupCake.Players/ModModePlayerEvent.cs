using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class ModModePlayerEvent : PlayerEvent<ModModeReceiveEvent>
    {
        internal ModModePlayerEvent(Player oldPlayer, Player player, ModModeReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}