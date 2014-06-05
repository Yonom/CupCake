using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class LeftPlayerEvent : PlayerEvent<LeftReceiveEvent>
    {
        public LeftPlayerEvent(Player player, LeftReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}