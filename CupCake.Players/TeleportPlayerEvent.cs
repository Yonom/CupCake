using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class TeleportPlayerEvent : PlayerEvent<TeleportUserReceiveEvent>
    {
        internal TeleportPlayerEvent(Player player, TeleportUserReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}