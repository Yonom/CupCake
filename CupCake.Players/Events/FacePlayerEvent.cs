using CupCake.EE.Events.Receive;

namespace CupCake.Players.Events
{
    public class FacePlayerEvent : PlayerEvent<FaceReceiveEvent>
    {
        public FacePlayerEvent(Player player, FaceReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}