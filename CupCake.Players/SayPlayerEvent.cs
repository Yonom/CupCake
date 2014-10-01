using CupCake.Messages.Receive;

namespace CupCake.Players
{
    /// <summary>
    ///     Occurs when a player chats in the world.
    /// </summary>
    /// <remarks>
    ///     This event is raised after a <see cref="SayReceiveEvent" /> has been raised.
    /// </remarks>
    public class SayPlayerEvent : PlayerEvent<SayReceiveEvent>
    {
        internal SayPlayerEvent(Player oldPlayer, Player player, SayReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}