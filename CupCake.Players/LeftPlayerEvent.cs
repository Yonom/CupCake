using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class LeftPlayerEvent : PlayerEvent<LeftReceiveEvent>
    {
        internal LeftPlayerEvent(Player oldPlayer, Player player, LeftReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}