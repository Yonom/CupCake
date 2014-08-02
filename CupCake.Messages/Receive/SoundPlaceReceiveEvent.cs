using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class SoundPlaceReceiveEvent : ReceiveEvent, IBlockPlaceReceiveEvent
    {
        public SoundPlaceReceiveEvent(Message message)
            : base(message)
        {
            this.PosX = message.GetInteger(0);
            this.PosY = message.GetInteger(1);
            this.Block = (SoundBlock)message.GetInteger(2);
            this.SoundId = message.GetUInt(3);
        }

        public Layer Layer
        {
            get { return Layer.Foreground; }
        }

        public SoundBlock Block { get; set; }
        public uint SoundId { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }

        Layer IBlockPlaceReceiveEvent.Layer
        {
            get { return this.Layer; }
            set { throw new NotSupportedException("Can not set Layer on this kind of block"); }
        }

        Block IBlockPlaceReceiveEvent.Block
        {
            get { return (Block)this.Block; }
            set { this.Block = (SoundBlock)value; }
        }
    }
}