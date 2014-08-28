using CupCake.Core;
using CupCake.Messages.Receive;

namespace CupCake.Players
{
    /// <summary>
    /// Occurs when a player gets teleported.
    /// </summary>
    /// <remarks>
    /// This event is raised after a <see cref="TeleportUserReceiveEvent"/> or a <see cref="TeleportEveryoneReceiveEvent"/> has been raised.
    /// </remarks>
    public class TeleportPlayerEvent : PlayerEvent<Point>
    {
        internal TeleportPlayerEvent(Player oldPlayer, Player player, Point innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}