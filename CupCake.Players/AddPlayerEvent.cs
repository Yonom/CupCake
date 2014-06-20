using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class AddPlayerEvent : PlayerEvent<AddReceiveEvent>
    {
        internal AddPlayerEvent(Player player, AddReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}