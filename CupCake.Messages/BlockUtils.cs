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

        /// <summary>
        /// Checks if the specified block is a coin door or coin gate
        /// </summary>
        /// <param name="id">The block to check</param>
        /// <returns><c>true</c> if the specified block is a coin door or coin gate, <c>false</c> otherwise</returns>
        public static bool IsCoinDoor(Block id)
        {
            return id == Block.CoinDoor || id == Block.CoinGate;
        }

        /// <summary>
        /// Checks if the specified block is a rotatable block
        /// </summary>
        /// <param name="id">The block to check</param>
        /// <returns><c>true</c> if the specified block is a rotatable block, <c>false</c> otherwise</returns>
        public static bool IsRotatable(Block id)
        {
            return id == Block.HazardSpike || id == Block.DecorSciFi2013BlueSlope ||
                   id == Block.DecorSciFi2013BlueStraight || id == Block.DecorSciFi2013YellowSlope ||
                   id == Block.DecorSciFi2013YellowStraight || id == Block.DecorSciFi2013GreenSlope ||
                   id == Block.DecorSciFi2013GreenStraight;
        }

        /// <summary>
        /// Checks if the specified block is a music block
        /// </summary>
        /// <param name="id">The block to check</param>
        /// <returns><c>true</c> if the specified block is a music block, <c>false</c> otherwise</returns>
        public static bool IsSound(Block id)
        {
            return id == Block.MusicPiano || id == Block.MusicDrum;
        }

        /// <summary>
        /// Checks if the specified block is a portal
        /// </summary>
        /// <param name="id">The block to check</param>
        /// <returns><c>true</c> if the specified block is a portal, <c>false</c> otherwise</returns>
        public static bool IsPortal(Block id)
        {
            return id == Block.Portal || id == Block.InvisiblePortal;
        }

        /// <summary>
        /// Checks if the specified block is a world portal
        /// </summary>
        /// <param name="id">The block to check</param>
        /// <returns><c>true</c> if the specified block is a world portal, <c>false</c> otherwise</returns>
        public static bool IsWorldPortal(Block id)
        {
            return id == Block.WorldPortal;
        }

        /// <summary>
        /// Checks if the specified block is a sign or a label
        /// </summary>
        /// <param name="id">The block to check</param>
        /// <returns><c>true</c> if the specified block is a sign or a label, <c>false</c> otherwise</returns>
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