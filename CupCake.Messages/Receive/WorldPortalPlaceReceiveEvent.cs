using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class WorldPortalPlaceReceiveEvent : ReceiveEvent, IBlockPlaceReceiveEvent
    {
        public WorldPortalPlaceReceiveEvent(Message message)
            : base(message)
        {
            this.PosX = message.GetInteger(0);
            this.PosY = message.GetInteger(1);
            this.Block = (WorldPortalBlock)message.GetInteger(2);
            this.WorldPortalTarget = message.GetString(3);
        }

        public Layer Layer
        {
            get { return Layer.Foreground; }
        }

        public WorldPortalBlock Block { get; set; }
        public string WorldPortalTarget { get; set; }

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
            set { this.Block = (WorldPortalBlock)value; }
        }
    }
}