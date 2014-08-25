using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class MovePlayerEvent : PlayerEvent<MoveReceiveEvent>
    {
        internal MovePlayerEvent(Player oldPlayer, Player player, MoveReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}