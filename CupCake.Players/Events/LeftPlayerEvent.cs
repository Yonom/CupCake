using CupCake.EE.Events.Receive;

namespace CupCake.Players.Events
{
    public class LeftPlayerEvent : PlayerEvent<LeftReceiveEvent>
    {
        public LeftPlayerEvent(Player player, LeftReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}