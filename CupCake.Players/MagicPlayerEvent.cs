using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class MagicPlayerEvent : PlayerEvent<MagicReceiveEvent>
    {
        internal MagicPlayerEvent(Player player, MagicReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}