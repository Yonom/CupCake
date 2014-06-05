using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class MagicPlayerEvent : PlayerEvent<MagicReceiveEvent>
    {
        public MagicPlayerEvent(Player player, MagicReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}