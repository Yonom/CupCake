using CupCake.Messages.Events.Receive;

namespace CupCake.Players.Events
{
    public class AutoTextPlayerEvent : PlayerEvent<AutoTextReceiveEvent>
    {
        public AutoTextPlayerEvent(Player player, AutoTextReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}