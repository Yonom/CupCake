using CupCake.Messages.Events.Receive;

namespace CupCake.Players.Events
{
    public class SayPlayerEvent : PlayerEvent<SayReceiveEvent>
    {
        public SayPlayerEvent(Player player, SayReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}