using CupCake.EE.Events.Receive;

namespace CupCake.Players.Events
{
    public class TeleportPlayerEvent : PlayerEvent<TeleportUserReceiveEvent>
    {
        public TeleportPlayerEvent(Player player, TeleportUserReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}