using PlayerIOClient;

namespace CupCake.Utils.Messages.Receive
{
    public sealed class RefreshShopReceiveMessage : ReceiveMessage
    {
        //No arguments; this is just a request to refresh the shop on the client-side.

        internal RefreshShopReceiveMessage(Message message)
            : base(message)
        {
        }
    }
}