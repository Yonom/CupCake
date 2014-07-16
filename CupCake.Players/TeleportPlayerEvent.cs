using CupCake.Core;

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