using CupCake.EE.Events.Receive;

namespace CupCake.Players.Events
{
    public class MovePlayerEvent : PlayerEvent<MoveReceiveEvent>
    {
        public MovePlayerEvent(Player player, MoveReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}