using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class AddPlayerEvent : PlayerEvent<AddReceiveEvent>
    {
        internal AddPlayerEvent(Player oldPlayer, Player player, AddReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}