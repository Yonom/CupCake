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
            this.PortalRotation = (PortalRotation)message.GetInteger(3);
            this.PortalId = message.GetInteger(4);
            this.PortalTarget = message.GetInteger(5);
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
            set { this.Block = (PortalBlock)value; }
        }

        public Layer Layer {
            get
            {
                return Layer.Foreground;
            }
        }

        public PortalBlock Block { get; set; }
        public int PortalId { get; set; }
        public PortalRotation PortalRotation { get; set; }
        public int PortalTarget { get; set; }
    }
}