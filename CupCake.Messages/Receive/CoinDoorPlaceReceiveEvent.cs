using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class CoinDoorPlaceReceiveEvent : ReceiveEvent, IBlockPlaceReceiveEvent
    {
        public CoinDoorPlaceReceiveEvent(Message message)
            : base(message)
        {
            PosX = message.GetInteger(0);
            PosY = message.GetInteger(1);
            Block = (CoinDoorBlock) message.GetInteger(2);
            CoinsToOpen = message.GetInteger(3);
        }

        public Layer Layer
        {
            get { return Layer.Foreground; }
        }

        public CoinDoorBlock Block { get; set; }
        public int CoinsToOpen { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }

        Layer IBlockPlaceReceiveEvent.Layer
        {
            get { return Layer; }
            set { throw new NotSupportedException("Can not set Layer on this kind of block"); }
        }

        Block IBlockPlaceReceiveEvent.Block
        {
            get { return (Block) Block; }
            set { Block = (CoinDoorBlock) value; }
        }
    }
}