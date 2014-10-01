using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    /// <summary>
    ///     Occurs when the shop is refreshed.
    /// </summary>
    public class RefreshShopReceiveEvent : ReceiveEvent
    {
        //No arguments; this is just a request to refresh the shop on the client-side.

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReceiveEvent" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public RefreshShopReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}