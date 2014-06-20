using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class CrownPlayerEvent : PlayerEvent<CrownReceiveEvent>
    {
        internal CrownPlayerEvent(Player player, CrownReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}