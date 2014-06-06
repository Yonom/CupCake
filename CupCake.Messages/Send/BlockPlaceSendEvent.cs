using System;
using CupCake.Messages.Blocks;
using CupCake.Messages.Receive;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class BlockPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        public BlockPlaceSendEvent(Layer layer, int x, int y, Block block)
        {
            this.Layer = BlockHelper.CorrectLayer(block, layer);
            this.X = x;
            this.Y = y;
            this.Block = block;
        }

        public Block Block { get; set; }
        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption, (int)this.Layer, this.X, this.Y, (int)this.Block);
        }
    }
}