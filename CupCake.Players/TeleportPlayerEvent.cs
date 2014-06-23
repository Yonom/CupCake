using CupCake.Core;
using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class TeleportPlayerEvent : PlayerEvent<Point>
    {
        internal TeleportPlayerEvent(Player player, Point innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}