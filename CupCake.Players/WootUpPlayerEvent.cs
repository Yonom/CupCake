using CupCake.Messages.Receive;

namespace CupCake.Players
{
    /// <summary>
    /// Occurs when a player gives the world a woot.
    /// </summary>
    /// <remarks>
    /// This event is raised after a <see cref="WootUpReceiveEvent"/> has been raised.
    /// </remarks>
    public class WootUpPlayerEvent : PlayerEvent<WootUpReceiveEvent>
    {
        internal WootUpPlayerEvent(Player oldPlayer, Player player, WootUpReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}