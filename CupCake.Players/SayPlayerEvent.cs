using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class SayPlayerEvent : PlayerEvent<SayReceiveEvent>
    {
        public SayPlayerEvent(Player player, SayReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}