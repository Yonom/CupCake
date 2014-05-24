using CupCake.Messages.Events.Receive;

namespace CupCake.Players.Events
{
    public class MagicPlayerEvent : PlayerEvent<MagicReceiveEvent>
    {
        public MagicPlayerEvent(Player player, MagicReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}