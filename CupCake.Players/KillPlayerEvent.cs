using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class KillPlayerEvent : PlayerEvent<KillReceiveEvent>
    {
        internal KillPlayerEvent(Player oldPlayer, Player player, KillReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}