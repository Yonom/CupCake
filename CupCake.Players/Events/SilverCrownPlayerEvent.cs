using CupCake.EE.Events.Receive;

namespace CupCake.Players.Events
{
    public class SilverCrownPlayerEvent : PlayerEvent<SilverCrownReceiveEvent>
    {
        public SilverCrownPlayerEvent(Player player, SilverCrownReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}