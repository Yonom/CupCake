using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class AutoTextPlayerEvent : PlayerEvent<AutoTextReceiveEvent>
    {
        public AutoTextPlayerEvent(Player player, AutoTextReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}