using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Messages.Blocks;

namespace CupCake.Messages
{
    internal class BlockHelper
    {
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
