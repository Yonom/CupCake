using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class RefreshShopReceiveEvent : ReceiveEvent
    {
        //No arguments; this is just a request to refresh the shop on the client-side.

        public RefreshShopReceiveEvent(Message message)
            : base(message)
        {
        }
    }
}