namespace CupCake.Physics.EEPhysics
{
    internal static class ItemId
    {
        public const int Piano = 77;
        public const int Drum = 83;
        public const int SwitchPurple = 113;
        public const int DoorPurple = 184;
        public const int GatePurple = 185;
        public const int DoorClub = 200;
        public const int GateClub = 201;
        public const int SpeedLeft = 114;
        public const int SpeedRight = 115;
        public const int SpeedUp = 116;
        public const int SpeedDown = 117;
        public const int Chain = 118;
        public const int Water = 119;
        public const int NinjaLadder = 120;
        public const int BrickComplete = 121;
        public const int Timedoor = 156;
        public const int Timegate = 157;
        public const int Coindoor = 43;
        public const int Coingate = 165;
        public const int BlueCoindoor = 213;
        public const int BlueCoingate = 214;
        public const int WineV = 98;
        public const int WineH = 99;
        public const int Diamond = 241;
        public const int Wave = 300;
        public const int Cake = 337;
        public const int Checkpoint = 360;
        public const int Spike = 361;
        public const int Fire = 368;
        public const int Mud = 369;
        public const int MudBubble = 370;
        public const int Portal = 242;
        public const int WorldPortal = 374;
        public const int ZombieGate = 206;
        public const int ZombieDoor = 207;
        public const int GlowylineBlueSlope = 375;
        public const int GlowyLineBlueStraight = 376;
        public const int GlowyLineYellowSlope = 377;
        public const int GlowyLineYellowStraight = 378;
        public const int GlowyLineGreenSlope = 379;
        public const int GlowyLineGreenStraight = 380;
        public const int PortalInvisible = 381;
        public const int TextSign = 385;
        public const int CyanKey = 408;
        public const int MagentaKey = 409;
        public const int YellowKey = 410;
        public const int InvisibleLeftArrow = 411;
        public const int InvisibleUpArrow = 412;
        public const int InvisibleRightArrow = 413;
        public const int InvisibleDot = 414;
        public const int Oneway_Cyan = 1001; 
        public const int Oneway_Red = 1002; 
        public const int Oneway_Yellow = 1003; 
        public const int Oneway_Pink = 1004;
        public const int CyanDoor = 1005;
        public const int MagentaDoor = 1006;
        public const int YellowDoor = 1007;
        public const int CyanGate = 1008;
        public const int MagentaGate = 1009;
        public const int YellowGate = 1010;
        public const int Death_Door = 1011;
        public const int Death_Gate = 1012;

        public static bool isSolid(int blockId)
        {
            return (9 <= blockId && blockId <= 97) || (122 <= blockId && blockId <= 217) || (1001 <= blockId && blockId <= 1012);
        }

        public static bool isClimbable(int id)
        {
            switch (id)
            {
                case ItemId.NinjaLadder:
                case ItemId.Chain:
                case ItemId.WineV:
                case ItemId.WineH:
                    return true;
            }

            return false;
        }

        public static bool canJumpThroughFromBelow(int itemId)
        {
            switch (itemId)
            {
                case 61:
                case 62:
                case 63:
                case 64:
                case 89:
                case 90:
                case 91:
                case 96:
                case 97:
                case 122:
                case 123:
                case 124:
                case 125:
                case 126:
                case 127:
                case 146:
                case 154:
                case 158:
                case 194:
                case 211:
                case 216:
                case Oneway_Cyan:
                case Oneway_Red:
                case Oneway_Yellow:
                case Oneway_Pink:
                    return true;
            }

            return false;
        }
    }
}
