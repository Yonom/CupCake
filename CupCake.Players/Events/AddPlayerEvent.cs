using CupCake.Messages.Events.Receive;

namespace CupCake.Players.Events
{
    public class AddPlayerEvent : PlayerEvent<AddReceiveEvent>
    {
        public AddPlayerEvent(Player player, AddReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}