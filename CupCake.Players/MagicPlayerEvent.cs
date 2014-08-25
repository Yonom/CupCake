using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class MagicPlayerEvent : PlayerEvent<MagicReceiveEvent>
    {
        internal MagicPlayerEvent(Player oldPlayer, Player player, MagicReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}