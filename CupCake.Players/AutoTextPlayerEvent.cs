using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class AutoTextPlayerEvent : PlayerEvent<AutoTextReceiveEvent>
    {
        internal AutoTextPlayerEvent(Player oldPlayer, Player player, AutoTextReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}