using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class CrownPlayerEvent : PlayerEvent<CrownReceiveEvent>
    {
        internal CrownPlayerEvent(Player oldPlayer, Player player, CrownReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}