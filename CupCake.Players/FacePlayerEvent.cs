using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class FacePlayerEvent : PlayerEvent<FaceReceiveEvent>
    {
        internal FacePlayerEvent(Player player, FaceReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}