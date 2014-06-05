using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class FacePlayerEvent : PlayerEvent<FaceReceiveEvent>
    {
        public FacePlayerEvent(Player player, FaceReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}