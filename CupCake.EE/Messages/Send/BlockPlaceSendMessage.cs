using System;
using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public class BlockPlaceSendMessage : SendMessage
    {
        public readonly Block Block;
        public readonly Layer Layer;
        public readonly int X;
        public readonly int Y;

        public BlockPlaceSendMessage(string encryption, Layer layer, int x, int y, Block block)
        {
            this.Encryption = encryption;
            this.Layer = layer;
            this.X = x;
            this.Y = y;
            this.Block = block;
        }

        public string Encryption { get; set; }

        internal override Message GetMessage()
        {
            return Message.Create(this.Encryption, Convert.ToInt32(CorrectLayer(this.Block, this.Layer)), this.X, this.Y,
                Convert.ToInt32(this.Block));
        }

        internal static Layer CorrectLayer(Block id, Layer layer)
        {
            if ((id > 0 && (int)id < 500) || id == Block.DecorationLabel)
            {
                return Layer.Foreground;
            }
            if ((int)id >= 500 && (int)id < 1000)
            {
                return Layer.Background;
            }
            return layer;
        }

        internal static bool IsCoinDoor(Block id)
        {
            return id == Block.BlockDoorCoinDoor || id == Block.BlockGateCoinGate;
        }

        internal static bool IsRotatable(Block id)
        {
            return id == Block.BlockHazardSpike || id == Block.DecorationSciFi2013BlueSlope ||
                   id == Block.DecorationSciFi2013BlueStraight || id == Block.DecorationSciFi2013YellowSlope ||
                   id == Block.DecorationSciFi2013YellowStraight || id == Block.DecorationSciFi2013GreenSlope ||
                   id == Block.DecorationSciFi2013GreenStraight;
        }

        internal static bool IsSound(Block id)
        {
            return id == Block.BlockMusicPiano || id == Block.BlockMusicDrum;
        }

        internal static bool IsPortal(Block id)
        {
            return id == Block.BlockPortal || id == Block.BlockInvisiblePortal;
        }

        internal static bool IsWorldPortal(Block id)
        {
            return id == Block.BlockWorldPortal;
        }

        internal static bool IsLabel(Block id)
        {
            return id == Block.DecorationSign || id == Block.DecorationLabel;
        }
    }
}