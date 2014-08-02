using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class PortalPlaceReceiveEvent : ReceiveEvent, IBlockPlaceReceiveEvent
    {
        public PortalPlaceReceiveEvent(Message message)
            : base(message)
        {
            this.PosX = message.GetInteger(0);
            this.PosY = message.GetInteger(1);
            this.Block = (PortalBlock)message.GetInteger(2);
            this.PortalRotation = (PortalRotation)message.GetUInt(3);
            this.PortalId = message.GetUInt(4);
            this.PortalTarget = message.GetUInt(5);
        }

        public Layer Layer
        {
            get { return Layer.Foreground; }
        }

        public PortalBlock Block { get; set; }
        public uint PortalId { get; set; }
        public PortalRotation PortalRotation { get; set; }
        public uint PortalTarget { get; set; }
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
            set { this.Block = (PortalBlock)value; }
        }
    }
}