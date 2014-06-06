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
            this.PosX = message.GetInteger(0);
            this.PosY = message.GetInteger(1);
            this.Block = (RotatableBlock)message.GetInteger(2);
            this.Rotation = message.GetInteger(3);
        }

        public int PosX { get; set; }
        public int PosY { get; set; }
        Layer IBlockPlaceReceiveEvent.Layer
        {
            get
            {
                return this.Layer;
            }
            set { throw new NotSupportedException("Can not set Layer on this kind of block"); }
        }
        Block IBlockPlaceReceiveEvent.Block
        {
            get { return (Block)this.Block; }
            set { this.Block = (RotatableBlock)value; }
        }

        public Layer Layer
        {
            get
            {
                return Layer.Foreground;
            }
        }

        public RotatableBlock Block { get; set; }
        public int Rotation { get; set; }
    }
}