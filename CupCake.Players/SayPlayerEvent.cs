using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class SayPlayerEvent : PlayerEvent<SayReceiveEvent>
    {
        internal SayPlayerEvent(Player player, SayReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}