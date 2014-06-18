using CupCake.Messages.Blocks;

namespace CupCake.Messages
{
    public class BlockUtils
    {
        public static Layer CorrectLayer(Block id, Layer layer)
        {
            if ((id > 0 && (int)id < 500) || id == Block.DecorLabel)
            {
                return Layer.Foreground;
            }
            if ((int)id >= 500 && (int)id < 1000)
            {
                return Layer.Background;
            }
            return layer;
        }

        public static bool IsCoinDoor(Block id)
        {
            return id == Block.CoinDoor || id == Block.CoinGate;
        }

        public static bool IsRotatable(Block id)
        {
            return id == Block.HazardSpike || id == Block.DecorSciFi2013BlueSlope ||
                   id == Block.DecorSciFi2013BlueStraight || id == Block.DecorSciFi2013YellowSlope ||
                   id == Block.DecorSciFi2013YellowStraight || id == Block.DecorSciFi2013GreenSlope ||
                   id == Block.DecorSciFi2013GreenStraight;
        }

        public static bool IsSound(Block id)
        {
            return id == Block.MusicPiano || id == Block.MusicDrum;
        }

        public static bool IsPortal(Block id)
        {
            return id == Block.Portal || id == Block.InvisiblePortal;
        }

        public static bool IsWorldPortal(Block id)
        {
            return id == Block.WorldPortal;
        }

        public static bool IsLabel(Block id)
        {
            return id == Block.DecorSign || id == Block.DecorLabel;
        }

        public static int PosToBlock(int pos)
        {
            return pos + 8 >> 4;
        }
    }
}