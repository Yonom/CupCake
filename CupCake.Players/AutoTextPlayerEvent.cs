using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class AutoTextPlayerEvent : PlayerEvent<AutoTextReceiveEvent>
    {
        internal AutoTextPlayerEvent(Player player, AutoTextReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}