using CupCake.Messages.Receive;

namespace CupCake.Players
{
    /// <summary>
    /// Occurs when a silver crown has been received.
    /// </summary>
    /// <remarks>
    /// This event is raised after a <see cref="SilverCrownReceiveEvent"/> has been raised.
    /// </remarks>
    public class SilverCrownPlayerEvent : PlayerEvent<SilverCrownReceiveEvent>
    {
        internal SilverCrownPlayerEvent(Player oldPlayer, Player player, SilverCrownReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}