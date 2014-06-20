using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class KillPlayerEvent : PlayerEvent<KillReceiveEvent>
    {
        internal KillPlayerEvent(Player player, KillReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}