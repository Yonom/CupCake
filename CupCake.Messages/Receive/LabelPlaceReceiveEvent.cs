using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class LabelPlaceReceiveEvent : ReceiveEvent, IBlockPlaceReceiveEvent
    {
        public LabelPlaceReceiveEvent(Message message)
            : base(message)
        {
            PosX = message.GetInteger(0);
            PosY = message.GetInteger(1);
            Block = (LabelBlock) message.GetInteger(2);
            Text = message.GetString(3);
        }

        public Layer Layer
        {
            get { return Layer.Foreground; }
        }

        public LabelBlock Block { get; set; }
        public string Text { get; set; }

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
            set { Block = (LabelBlock) value; }
        }
    }
}