using CupCake.Messages.Receive;

namespace CupCake.Players
{
    /// <summary>
    ///     Occurs when a potion has been consumed by a player.
    /// </summary>
    /// <remarks>
    ///     This event is raised after a <see cref="PotionReceiveEvent" /> has been raised.
    /// </remarks>
    public class PotionPlayerEvent : PlayerEvent<PotionReceiveEvent>
    {
        internal PotionPlayerEvent(Player oldPlayer, Player player, PotionReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}