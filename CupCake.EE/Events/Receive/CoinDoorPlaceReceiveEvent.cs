using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public class CoinDoorPlaceReceiveEvent : BlockPlaceReceiveEvent
    {
        public CoinDoorPlaceReceiveEvent(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.CoinDoorBlock = (CoinDoorBlock)message.GetInteger(2);
            this.CoinsToOpen = message.GetInteger(3);
        }

        public CoinDoorBlock CoinDoorBlock { get; private set; }
        public int CoinsToOpen { get; private set; }
    }
}