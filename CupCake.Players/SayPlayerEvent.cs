using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class SayPlayerEvent : PlayerEvent<SayReceiveEvent>
    {
        internal SayPlayerEvent(Player oldPlayer, Player player, SayReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}