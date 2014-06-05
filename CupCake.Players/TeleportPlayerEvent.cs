using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class TeleportPlayerEvent : PlayerEvent<TeleportUserReceiveEvent>
    {
        public TeleportPlayerEvent(Player player, TeleportUserReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}