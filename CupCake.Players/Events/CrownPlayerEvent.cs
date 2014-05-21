using CupCake.EE.Events.Receive;

namespace CupCake.Players.Events
{
    public class CrownPlayerEvent : PlayerEvent<CrownReceiveEvent>
    {
        public CrownPlayerEvent(Player player, CrownReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}