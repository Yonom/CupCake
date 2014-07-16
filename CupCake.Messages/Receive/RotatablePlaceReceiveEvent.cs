using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class RotatablePlaceReceiveEvent : ReceiveEvent, IBlockPlaceReceiveEvent
    {
        public RotatablePlaceReceiveEvent(Message message)
            : base(message)
        {
            PosX = message.GetInteger(0);
            PosY = message.GetInteger(1);
            Block = (RotatableBlock) message.GetInteger(2);
            Rotation = message.GetInteger(3);
        }

        public Layer Layer
        {
            get { return Layer.Foreground; }
        }

        public RotatableBlock Block { get; set; }
        public int Rotation { get; set; }

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
            set { Block = (RotatableBlock) value; }
        }
    }
}