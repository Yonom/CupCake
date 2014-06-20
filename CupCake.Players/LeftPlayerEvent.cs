using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class LeftPlayerEvent : PlayerEvent<LeftReceiveEvent>
    {
        internal LeftPlayerEvent(Player player, LeftReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}