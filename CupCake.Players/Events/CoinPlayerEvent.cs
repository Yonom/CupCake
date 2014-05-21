using CupCake.EE.Events.Receive;

namespace CupCake.Players.Events
{
    public class CoinPlayerEvent : PlayerEvent<CoinReceiveEvent>
    {
        public CoinPlayerEvent(Player player, CoinReceiveEvent innerEvent)
            : base(player, innerEvent)
        {
        }
    }
}