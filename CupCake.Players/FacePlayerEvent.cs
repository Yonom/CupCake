using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class FacePlayerEvent : PlayerEvent<FaceReceiveEvent>
    {
        internal FacePlayerEvent(Player oldPlayer, Player player, FaceReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}