namespace CupCake.Messages.Blocks
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
        /// Checks if the specified block is a coin door or coin gate.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a coin door or coin gate; otherwise, <c>false</c>.</returns>
        public static bool IsCoinDoor(Block id)
        {
            return id == Block.CoinDoor || id == Block.CoinGate ||
                   id == Block.BlueCoinDoor || id == Block.BlueCoinGate;
        }

        /// <summary>
        /// Checks if the specified block is a rotatable block.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a rotatable block; otherwise, <c>false</c>.</returns>
        public static bool IsRotatable(Block id)
        {
            return id == Block.HazardSpike || id == Block.DecorSciFi2013BlueSlope ||
                   id == Block.DecorSciFi2013BlueStraight || id == Block.DecorSciFi2013YellowSlope ||
                   id == Block.DecorSciFi2013YellowStraight || id == Block.DecorSciFi2013GreenSlope ||
                   id == Block.DecorSciFi2013GreenStraight;
        }
        
        /// <summary>
        /// Checks if the specified block is a spike block.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a spike block; otherwise, <c>false</c>.</returns>
        public static bool IsSpike(Block id)
        {
            return id == Block.HazardSpike;
        }
        
        /// <summary>
        /// Checks if the specified block is a SciFi sloped block.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a SciFi sloped block; otherwise, <c>false</c>.</returns>
        public static bool IsSciFiSlope(Block id)
        {
            return id == Block.DecorSciFi2013BlueSlope ||
                   id == Block.DecorSciFi2013YellowSlope ||
                   id == Block.DecorSciFi2013GreenSlope;
        }
        
        /// <summary>
        /// Checks if the specified block is a SciFi straight block.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a SciFi straight block; otherwise, <c>false</c>.</returns>
        public static bool IsSciFiStraight(Block id)
        {
            return id == Block.DecorSciFi2013BlueStraight ||
                   id == Block.DecorSciFi2013YellowStraight ||
                   id == Block.DecorSciFi2013GreenStraight;
        }

        /// <summary>
        /// Determines whether the specified block is a piano block.
        /// </summary>
        /// <param name="id">The block.</param>
        /// <returns><c>true</c> if the specified block is a piano block; otherwise, <c>false</c>.</returns>
        public static bool IsPiano(Block id)
        {
            return id == Block.MusicPiano;
        }

        /// <summary>
        /// Determines whether the specified block is a drum block.
        /// </summary>
        /// <param name="id">The block.</param>
        /// <returns><c>true</c> if the specified block is a drum block; otherwise, <c>false</c>.</returns>
        public static bool IsDrum(Block id)
        {
            return id == Block.MusicDrum;
        }

        /// <summary>
        /// Checks if the specified block is a music block.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a music block; otherwise, <c>false</c>.</returns>
        public static bool IsSound(Block id)
        {
            return id == Block.MusicPiano || id == Block.MusicDrum;
        }

        /// <summary>
        /// Checks if the specified block is a portal.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a portal; otherwise, <c>false</c>.</returns>
        public static bool IsPortal(Block id)
        {
            return id == Block.Portal || id == Block.InvisiblePortal;
        }

        /// <summary>
        /// Checks if the specified block is a world portal.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a world portal; otherwise, <c>false</c>.</returns>
        public static bool IsWorldPortal(Block id)
        {
            return id == Block.WorldPortal;
        }

        /// <summary>
        /// Checks if the specified block is a sign or a label.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a sign or a label; otherwise, <c>false</c>.</returns>
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