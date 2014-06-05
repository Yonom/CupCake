using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class CrownPlayerEvent : PlayerEvent<CrownReceiveEvent>
    {
        public CrownPlayerEvent(Player player, CrownReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}