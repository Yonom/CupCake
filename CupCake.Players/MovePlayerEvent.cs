using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class MovePlayerEvent : PlayerEvent<MoveReceiveEvent>
    {
        internal MovePlayerEvent(Player player, MoveReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}