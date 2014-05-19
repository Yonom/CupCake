using System;
using CupCake.EE.Blocks;
using CupCake.World.Blocks;

namespace CupCake.World
{
    public class BlockPlaceEventArgs : EventArgs
    {
        public BlockPlaceEventArgs(int posX, int posY, Layer layer, WorldBlock block)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.Layer = layer;
            this.Block = block;
        }

        public int PosX { get; set; }
        public int PosY { get; set; }
        public Layer Layer { get; set; }
        public WorldBlock Block { get; set; }
    }
}