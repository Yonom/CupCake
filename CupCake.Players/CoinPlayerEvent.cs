using CupCake.Messages.Receive;

namespace CupCake.Players
{
    public class CoinPlayerEvent : PlayerEvent<CoinReceiveEvent>
    {
        internal CoinPlayerEvent(Player player, CoinReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}