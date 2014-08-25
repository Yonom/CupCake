using CupCake.Core;

namespace CupCake.Players
{
    public class TeleportPlayerEvent : PlayerEvent<Point>
    {
        internal TeleportPlayerEvent(Player oldPlayer, Player player, Point innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}