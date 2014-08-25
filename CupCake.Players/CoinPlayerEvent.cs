using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class CoinPlayerEvent : PlayerEvent<CoinReceiveEvent>
    {
        internal CoinPlayerEvent(Player oldPlayer, Player player, CoinReceiveEvent innerEvent)
            : base(oldPlayer, player, innerEvent)
        {
        }
    }
}