using CupCake.EE.Events.Receive;

namespace CupCake.Players.Events
{
    public class KillPlayerEvent : PlayerEvent<KillReceiveEvent>
    {
        public KillPlayerEvent(Player player, KillReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}