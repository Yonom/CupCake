using CupCake.Messages.Blocks;

namespace CupCake.Physics
{
    class PhysicsUtils
    {
        public static bool IsSolid(Block block)
        {
            return block >= (Block)9 && block <= (Block)97 || block >= (Block)122 && block <= (Block)217;
        }

        public static bool IsClimbable(Block block)
        {
            switch (block)
            {
                case Block.LadderCastle:
                case Block.LadderNinja:
                case Block.LadderJungleHorizontal:
                case Block.LadderJungleVertical:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsBackgroundRotateable(Block block)
        {
            return false;
        }

        public static bool IsDecorationRotateable(Block param1)
        {
            switch (param1)
            {
                case Block.DecorSciFi2013BlueStraight:
                case Block.DecorSciFi2013BlueSlope:
                case Block.DecorSciFi2013GreenStraight:
                case Block.DecorSciFi2013GreenSlope:
                case Block.DecorSciFi2013YellowStraight:
                case Block.DecorSciFi2013YellowSlope:
                    return true;
                default:
                    return false;
            }
        }
    }
}
