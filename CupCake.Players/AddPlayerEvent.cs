using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class AddPlayerEvent : PlayerEvent<AddReceiveEvent>
    {
        public AddPlayerEvent(Player player, AddReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}