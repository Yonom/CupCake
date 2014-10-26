using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEPhysics
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

        public static bool isSolid(int blockId)
        {
            return (9 <= blockId && blockId <= 97) || (122 <= blockId && blockId <= 217);
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
    }
}
