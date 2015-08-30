using CupCake.Messages.Send;

namespace CupCake.Messages.Blocks
{
    public class BlockUtils
    {
        public static Layer CorrectLayer(Block id, Layer layer)
        {
            if ((id > 0 && (int)id < 500) || (int)id >= 1000)
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
        ///     Checks if the specified block is a coin door or coin gate.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a coin door or coin gate; otherwise, <c>false</c>.</returns>
        public static bool IsCoinDoorOrGate(Block id)
        {
            switch (id)
            {
                case Block.CoinDoor:
                case Block.CoinGate:
                case Block.BlueCoinDoor:
                case Block.BlueCoinGate:
                case (Block)417:
                case (Block)418:
                case (Block)419:
                case (Block)420:
                case (Block)421:
                case (Block)422:
                case (Block)423:
                case (Block)1027:
                case (Block)1028:
                case (Block)113:
                case (Block)184:
                case (Block)185:
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Checks if the specified block is a death door or gate.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a death door or gate; otherwise, <c>false</c>.</returns>
        public static bool IsDeathDoorOrGate(Block id)
        {
            switch (id)
            {
                case Block.DeathDoor:
                case Block.DeathGate:
                    return true;
            }

            return false;

        }

        /// <summary>
        ///     Checks if the specified block is a purple door, gate, or switch.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a purple door, gate, or switch; otherwise, <c>false</c>.</returns>
        public static bool IsPurpleDoorOrGateOrSwitch(Block id)
        {
            switch (id)
            {
                case Block.DoorPurpleSwitch:
                case Block.GatePurpleSwitch:
                case Block.SwitchPurple:
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Checks if the specified block is a key
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns><c>true</c> if the specified block is a key; otherwise, <c>false</c>.</returns>
        public static bool IsKey(Key key)
        {
            switch (key)
            {
                case Key.Blue:
                case Key.Cyan:
                case Key.Green:
                case Key.Magenta:
                case Key.Red:
                case Key.Yellow:
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Checks if the specified block is a rotatable block.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a rotatable block; otherwise, <c>false</c>.</returns>
        public static bool IsRotatable(Block id)
        {
            switch (id)
            {
                case Block.HazardSpike:
                case Block.DecorSciFi2013BlueSlope:
                case Block.DecorSciFi2013BlueStraight:
                case Block.DecorSciFi2013YellowSlope:
                case Block.DecorSciFi2013YellowStraight:
                case Block.DecorSciFi2013GreenSlope:
                case Block.DecorSciFi2013GreenStraight:
                case Block.OneWayCyan:
                case Block.OneWayPink:
                case Block.OneWayRed:
                case Block.OneWayYellow:
                case (Block)276:
                case (Block)277:
                case (Block)279:
                case (Block)280:
                case (Block)440:
                case (Block)275:
                case (Block)329:
                case (Block)273:
                case (Block)328:
                case (Block)327:
                case (Block)338:
                case (Block)339:
                case (Block)340:
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Checks if the specified block is a spike block.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a spike block; otherwise, <c>false</c>.</returns>
        public static bool IsSpike(Block id)
        {
            return id == Block.HazardSpike;
        }

        /// <summary>
        ///     Checks if the specified block is a SciFi sloped block.
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
        ///     Checks if the specified block is a SciFi straight block.
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
        ///     Determines whether the specified block is a piano block.
        /// </summary>
        /// <param name="id">The block.</param>
        /// <returns><c>true</c> if the specified block is a piano block; otherwise, <c>false</c>.</returns>
        public static bool IsPiano(Block id)
        {
            return id == Block.MusicPiano;
        }

        /// <summary>
        ///     Determines whether the specified block is a drum block.
        /// </summary>
        /// <param name="id">The block.</param>
        /// <returns><c>true</c> if the specified block is a drum block; otherwise, <c>false</c>.</returns>
        public static bool IsDrum(Block id)
        {
            return id == Block.MusicDrum;
        }

        /// <summary>
        ///     Checks if the specified block is a music block.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a music block; otherwise, <c>false</c>.</returns>
        public static bool IsSound(Block id)
        {
            return id == Block.MusicPiano || id == Block.MusicDrum;
        }

        /// <summary>
        ///     Checks if the specified block is a portal.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a portal; otherwise, <c>false</c>.</returns>
        public static bool IsPortal(Block id)
        {
            return id == Block.Portal || id == Block.InvisiblePortal;
        }

        /// <summary>
        ///     Checks if the specified block is a world portal.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a world portal; otherwise, <c>false</c>.</returns>
        public static bool IsWorldPortal(Block id)
        {
            return id == Block.WorldPortal;
        }

        /// <summary>
        ///     Checks if the specified block is a sign.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a sign; otherwise, <c>false</c>.</returns>
        public static bool IsSign(Block id)
        {
            return id == Block.DecorSign;
        }

        /// <summary>
        ///     Checks if the specified block is a label.
        /// </summary>
        /// <param name="id">The block to check.</param>
        /// <returns><c>true</c> if the specified block is a label; otherwise, <c>false</c>.</returns>
        public static bool IsLabel(Block id)
        {
            return id == Block.DecorLabel;
        }

        /// <summary>
        ///     Takes a exact pixel position and converts the coordinate to block units
        /// </summary>
        /// <param name="pos">Exact pixel position</param>
        /// <returns></returns>
        public static int PosToBlock(int pos)
        {
            return pos + 8 >> 4;
        }
    }
}