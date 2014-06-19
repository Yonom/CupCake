using System;
using System.Collections.Generic;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Keys;
using CupCake.Messages;
using CupCake.Messages.Blocks;
using CupCake.Players;
using CupCake.Room;
using CupCake.World;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace CupCake.Physics
{
    public sealed class PhysicsPlayer : CupCakeServicePart<Player>
    {
        // CupCake variables
        private WorldService _world;
        private RoomService _room;
        private KeyService _keys;

        // Settings
        internal const int PhysicsMsPerTick = 10;
        private static readonly double _physicsBaseDrag = Math.Pow(0.9981, PhysicsMsPerTick) * 1.00016;
        private static readonly double _noModifierDrag = Math.Pow(0.99, PhysicsMsPerTick) * 1.00016;
        private static readonly double _waterDrag = Math.Pow(0.995, PhysicsMsPerTick) * 1.00016;
        private static readonly double _mudDrag = Math.Pow(0.975, PhysicsMsPerTick) * 1.00016;

        private const double PhysicsJumpHeight = 26;
        private const double PhysicsGravity = 2;
        private const double PhysicsBoost = 16;
        private const double PhysicsWaterBuoyancy = -0.5;
        private const double PhysicsMudBuoyancy = 0.4;

        private const int Size = 16;
        private const double MaxThrust = 0.2;
        private const double ThrustBurnOff = 0.01;
        private const double VariableMultiplier = 7.752;

        private const double WaterBuoyancy = PhysicsWaterBuoyancy;
        private const double MudBuoyancy = PhysicsMudBuoyancy;
        private const double BoostSpeed = PhysicsBoost;
        private const double Gravity = PhysicsGravity;

        // Physics variables
        private readonly Queue<Block> _queue = new Queue<Block>();

        private double _speedX;
        private double _speedY;
        private double _modifierX;
        private double _modifierY;

        private double _oldHorizontalAcceleration;
        private double _oldVerticalAcceleration;
        private double _horizontalAcceleration;
        private double _verticalAcceleration;

        private int _morX;
        private int _morY;
        private Block _currentBlockId;
        private int _onewayY;

        private Point _lastPortal = new Point();
        private double _currentThrust = MaxThrust;

        private bool _doneX;
        private bool _doneY;

        private double _reminderX;
        private double _currentSX;
        private double _reminderY;
        private double _currentSY;

        private double _osX;
        private double _osY;
        private double _oX;
        private double _oY;
        private double _tX;
        private double _tY;

        public bool IsDead { get; private set; }
        public double Horizontal { get; private set; }
        public double Vertical { get; private set; }

        public bool IsZombie
        {
            get
            {
                if (this.IsGodOrMod)
                    return false;

                return this.BasePlayer.ZombiePotion;
            }
        }

        public double X { get; private set; }
        public double Y { get; private set; }

        public int BlockX
        {
            get { return BlockUtils.PosToBlock((int)this.X); }
        }

        public int BlockY
        {
            get { return BlockUtils.PosToBlock((int)this.Y); }
        }

        public double SpeedX
        {
            get { return this._speedX * VariableMultiplier; }
            private set { this._speedX = value / VariableMultiplier; }
        }

        public double SpeedY
        {
            get { return this._speedY * VariableMultiplier; }
            private set { this._speedY = value / VariableMultiplier; }
        }

        public double ModifierX
        {
            get { return this._modifierX * VariableMultiplier; }
            private set { this._modifierX = value / VariableMultiplier; }
        }

        public double ModifierY
        {
            get { return this._modifierY * VariableMultiplier; }
            private set { this._modifierY = value / VariableMultiplier; }
        }

        private bool IsGodOrMod
        {
            get { return this.BasePlayer.IsGod || this.BasePlayer.IsMod; }
        }

        public Player BasePlayer { get; set; }

        protected override void Enable()
        {
            this._world = this.ServiceLoader.Get<WorldService>();
            this._room = this.ServiceLoader.Get<RoomService>();
            this._keys = this.ServiceLoader.Get<KeyService>();
            
            this.BasePlayer = this.Host;
            this.UpdateVars();

            this.Events.Bind<MovePlayerEvent>(this.OnMove, EventPriority.High);
            this.Events.Bind<TickPhysicsEvent>(this.OnTick, EventPriority.High);
        }

        private void OnTick(object sender, TickPhysicsEvent e)
        {
            this.Tick();
        }

        private void OnMove(object sender, MovePlayerEvent e)
        {
            if (e.Player == this.BasePlayer)
            {
                this.UpdateVars();
            }
        }

        private void UpdateVars()
        {
            this.X = this.BasePlayer.PosX;
            this.Y = this.BasePlayer.PosY;
            this.SpeedX = this.BasePlayer.SpeedX;
            this.SpeedY = this.BasePlayer.SpeedY;
            this.ModifierX = this.BasePlayer.ModifierX;
            this.ModifierY = this.BasePlayer.ModifierY;
            this.Horizontal = this.BasePlayer.Horizontal;
            this.Vertical = this.BasePlayer.Vertical;
            this.IsDead = this.BasePlayer.IsDead;
        }

        private double GetSpeedMultiplier()
        {
            if (this.IsZombie)
                return 0.6;

            return 1;
        }


        // TODO: Needs cleanup
        private bool Overlaps()
        {
            bool onewaySet = false;

            if (this.X < 0 || this.Y < 0 || this.X >= this._world.RoomWidth * 16 - 8 ||
                this.Y >= this._world.RoomHeight * 16 - 8)
            {
                return true;
            }

            if (this.BasePlayer.IsGod || this.BasePlayer.IsMod)
            {
                return false;
            }

            double bx = ((this.X) / 16);
            double by = ((this.Y) / 16);
            for (int xx = -2; xx < 1; xx++)
            {
                for (int yy = -2; yy < 1; yy++)
                {
                    if (bx + xx > 0 && bx + xx < this._world.RoomWidth && by + yy > 0 &&
                        by + yy <= this._world.RoomHeight)
                    {
                        for (int xTest = 0; xTest < 16; xTest++)
                        {
                            for (int yTest = 0; yTest < 16; yTest++)
                            {
                                if (HitTest((int)(xTest + this.X + xx * 16), (int)(yTest + this.Y + yy * 16)))
                                {
                                    WorldBlock currentBlock = this._world[0,
                                        (int)(((xx * 16) + this.X + xTest) / 16),
                                        (int)(((yy * 16) + this.Y + yTest) / 16)];

                                    Block b = currentBlock.Block;
                                    if (PhysicsUtils.IsSolid(b))
                                    {
                                        switch (b)
                                        {
                                            case Block.DoorRed:
                                                if (this._keys.RedKey)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.DoorGreen:
                                                if (this._keys.GreenKey)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.DoorBlue:
                                                if (this._keys.BlueKey)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.GateRed:
                                                if (!this._keys.RedKey)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.GateGreen:
                                                if (!this._keys.GreenKey)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.GateBlue:
                                                if (!this._keys.BlueKey)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.TimedDoor:
                                                if (this._keys.TimeDoor)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.TimedGate:
                                                if (!this._keys.TimeDoor)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.DoorPurpleSwitch:
                                                if (this.BasePlayer.SwitchOpened)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.GatePurpleSwitch:
                                                if (!this.BasePlayer.SwitchOpened)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.DoorBuildersClub:
                                                if (this.BasePlayer.IsClubMember)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.GateBuildersClub:
                                                if (!this.BasePlayer.IsClubMember)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.CoinDoor:
                                                if (currentBlock.CoinsToCollect <= this.BasePlayer.Coins)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.CoinGate:
                                                if (currentBlock.CoinsToCollect > this.BasePlayer.Coins)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.GateZombie:
                                                if (this.BasePlayer.ZombiePotion)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.DoorZombie:
                                                if (!this.BasePlayer.ZombiePotion)
                                                {
                                                    continue;
                                                }
                                                break;
                                            case Block.CandyOneWayPink:
                                            case Block.CandyOneWayRed:
                                            case Block.CandyOneWayBlue:
                                            case Block.CandyOneWayGreen:
                                            case Block.SciFiOneWayRed:
                                            case Block.SciFiOneWayBlue:
                                            case Block.SciFiOneWayGreen:
                                            case Block.NinjaWhite:
                                            case Block.NinjaGrey:
                                            case Block.CowboyBrownLit:
                                            case Block.CowboyRedLit:
                                            case Block.CowboyBlueLit:
                                            case Block.CowboyBrownDark:
                                            case Block.CowboyRedDark:
                                            case Block.CowboyBlueDark:
                                            case Block.IndustrialOneWay:
                                            case Block.Timbered:
                                            case Block.CastleOneWay:
                                            case Block.JungleRuinsOneWay:
                                                if (this.SpeedY < 0 || by <= this._onewayY)
                                                {
                                                    if (this._onewayY == -1)
                                                    {
                                                        this._onewayY = (int)by;
                                                    }
                                                    onewaySet = true;
                                                    continue;
                                                }
                                                break;
                                            case Block.MusicPiano:
                                            case Block.MusicDrum:
                                                continue;
                                        }
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (!onewaySet)
            {
                this._onewayY = -1;
            }
            return false;
        }

        private void StepX()
        {
            if (this._currentSX > 0)
            {
                if (this._currentSX + this._reminderX >= 1)
                {
                    X = X + (1 - this._reminderX);
                    X = Math.Floor(X);
                    this._currentSX = this._currentSX - (1 - this._reminderX);
                    this._reminderX = 0;
                }
                else
                {
                    X = X + this._currentSX;
                    this._currentSX = 0;
                }
            }
            else if (this._currentSX < 0)
            {
                if (this._reminderX != 0 && this._reminderX + this._currentSX < 0)
                {
                    this._currentSX = this._currentSX + this._reminderX;
                    X = X - this._reminderX;
                    X = Math.Floor(X);
                    this._reminderX = 1;
                }
                else
                {
                    X = X + this._currentSX;
                    this._currentSX = 0;
                }
            }

            if (this.Overlaps())
            {
                X = this._oX;
                _speedX = 0;
                this._currentSX = this._osX;
                this._doneX = true;
            }
        }

        private void StepY()
        {
            if (this._currentSY > 0)
            {
                if (this._currentSY + this._reminderY >= 1)
                {
                    Y = Y + (1 - this._reminderY);
                    Y = Math.Floor(Y);
                    this._currentSY = this._currentSY - (1 - this._reminderY);
                    this._reminderY = 0;
                }
                else
                {
                    Y = Y + this._currentSY;
                    this._currentSY = 0;
                }
            }
            else if (this._currentSY < 0)
            {
                if (this._reminderY != 0 && this._reminderY + this._currentSY < 0)
                {
                    Y = Y - this._reminderY;
                    Y = Math.Floor(Y);
                    this._currentSY = this._currentSY + this._reminderY;
                    this._reminderY = 1;
                }
                else
                {
                    Y = Y + this._currentSY;
                    this._currentSY = 0;
                }
            }

            if (this.Overlaps())
            {
                Y = this._oY;
                _speedY = 0;
                this._currentSY = this._osY;
                this._doneY = true;
            }
        }

        // TODO: Needs cleanup
        private void ProcessPortals()
        {
            var targetPortalList = new List<Point>();
            this._currentBlockId = this._world[0, this.BlockX, this.BlockY].Block;

            if (!this.IsGodOrMod && this._currentBlockId == Block.Portal)
            {
                if (this._lastPortal.X == 0 && this._lastPortal.Y == 0)
                {
                    this._lastPortal = new Point(this.BlockX, this.BlockY);

                    WorldBlock currentBlock = this._world[0, this.BlockX, this.BlockY];
                    int currentTarget = currentBlock.PortalTarget;

                    for (int x = 1; x < this._world.RoomWidth; x++)
                    {
                        for (int y = 1; y < this._world.RoomHeight; y++)
                        {
                            WorldBlock block = this._world[0, x, y];
                            if (BlockUtils.IsPortal(block.Block) && block.PortalId == currentTarget)
                            {
                                targetPortalList.Add(new Point(x, y));
                            }
                        }
                    }
                    foreach (var currentLoopPortal in targetPortalList)
                    {
                        PortalRotation lastRotation =
                            this._world[0, this._lastPortal.X, this._lastPortal.Y].PortalRotation;
                        PortalRotation currentRotation =
                            this._world[0, currentLoopPortal.X, currentLoopPortal.Y].PortalRotation;
                        if (lastRotation < currentRotation)
                        {
                            lastRotation = lastRotation + 4;
                        }
                        const double dirChangeMultiplier = 1.42;

                        int rotDif = lastRotation - currentRotation;
                        switch (rotDif)
                        {
                            case 1:
                                this.SpeedX = this.SpeedY * dirChangeMultiplier;
                                this.SpeedY = (-this.SpeedX) * dirChangeMultiplier;
                                this.ModifierX = this.ModifierY * dirChangeMultiplier;
                                this.ModifierY = (-this.SpeedY) * dirChangeMultiplier;
                                this._reminderY = -this._reminderY;
                                this._currentSY = -this._currentSY;
                                break;

                            case 3:
                                this.SpeedX = (-this.SpeedY) * dirChangeMultiplier;
                                this.SpeedY = this.SpeedX * dirChangeMultiplier;
                                this.ModifierX = (-this.ModifierY) * dirChangeMultiplier;
                                this.ModifierY = this.ModifierX * dirChangeMultiplier;
                                this._reminderX = -this._reminderX;
                                this._currentSX = -this._currentSX;
                                break;

                            case 2:
                                this.SpeedX = (-this.SpeedX) * dirChangeMultiplier;
                                this.SpeedY = (-this.SpeedY) * dirChangeMultiplier;
                                this.ModifierX = (-this.ModifierX) * dirChangeMultiplier;
                                this.ModifierY = (-this.ModifierY) * dirChangeMultiplier;
                                this._reminderY = -this._reminderY;
                                this._currentSY = -this._currentSY;
                                this._reminderX = -this._reminderX;
                                this._currentSX = -this._currentSX;
                                break;
                        }

                        X = currentLoopPortal.X;
                        Y = currentLoopPortal.Y;
                        this._lastPortal = currentLoopPortal;
                        break;
                    }
                }
            }
            else
            {
                this._lastPortal = new Point(0, 0);
            }
        }

        // TODO: Needs cleanup
        private void Tick()
        {
            Block delayed = 0;

            if (this._queue.Count >= 1)
            {
                delayed = this._queue.Dequeue();
            }
            this._currentBlockId = this._world[0, this.BlockX, this.BlockY].Block;
            this._queue.Enqueue(this._currentBlockId);
            if (this._currentBlockId == Block.GravityDot || PhysicsUtils.IsClimbable(this._currentBlockId))
            {
                delayed = this._queue.Peek();
            }
            if (this.IsDead)
            {
                this.Horizontal = 0;
                this.Vertical = 0;
            }
            if (this.IsGodOrMod)
            {
                this._morX = 0;
                this._morY = 0;
                this._oldHorizontalAcceleration = 0;
                this._oldVerticalAcceleration = 0;
            }
            else
            {
                switch (this._currentBlockId)
                {
                    case Block.GravityLeft:
                        this._morX = -(int)Gravity;
                        this._morY = 0;
                        break;

                    case Block.GravityUp:
                        this._morX = 0;
                        this._morY = -(int)Gravity;
                        break;

                    case Block.GravityRight:
                        this._morX = (int)Gravity;
                        this._morY = 0;
                        break;

                    case Block.GravityNothing:
                    case Block.BoostLeft:
                    case Block.BoostUp:
                    case Block.BoostRight:
                    case Block.BoostDown:
                    case Block.LadderCastle:
                    case Block.LadderNinja:
                    case Block.LadderJungleHorizontal:
                    case Block.LadderJungleVertical:
                        this._morX = 0;
                        this._morY = 0;
                        break;

                    case Block.Water:
                        this._morX = 0;
                        this._morY = (int)WaterBuoyancy;
                        break;

                    case Block.Swamp:
                        this._morX = 0;
                        this._morY = (int)MudBuoyancy;
                        break;

                    case Block.HazardFire:
                    case Block.HazardSpike:
                        if (!this.BasePlayer.ProtectionPotion)
                        {
                            this.IsDead = true;
                        }
                        break;

                    default:
                        this._morX = 0;
                        this._morY = (int)Gravity;
                        break;
                }
                switch (delayed)
                {
                    case Block.GravityLeft:
                        this._oldHorizontalAcceleration = -Gravity;
                        this._oldVerticalAcceleration = 0;
                        break;

                    case Block.GravityUp:
                        this._oldHorizontalAcceleration = 0;
                        this._oldVerticalAcceleration = -Gravity;
                        break;

                    case Block.GravityRight:

                        this._oldHorizontalAcceleration = Gravity;
                        this._oldVerticalAcceleration = 0;
                        break;

                    case Block.BoostLeft:
                    case Block.BoostUp:
                    case Block.BoostRight:
                    case Block.BoostDown:
                    case Block.LadderCastle:
                    case Block.LadderNinja:
                    case Block.LadderJungleHorizontal:
                    case Block.LadderJungleVertical:
                        this._oldHorizontalAcceleration = 0;
                        this._oldVerticalAcceleration = 0;
                        break;

                    case Block.Water:
                        this._oldHorizontalAcceleration = 0;
                        this._oldVerticalAcceleration = WaterBuoyancy;
                        break;

                    case Block.Swamp:
                        this._oldHorizontalAcceleration = 0;
                        this._oldVerticalAcceleration = MudBuoyancy;
                        break;
                    default:
                        this._oldHorizontalAcceleration = 0;
                        this._oldVerticalAcceleration = Gravity;
                        break;
                }
            }
            if (this._oldVerticalAcceleration == WaterBuoyancy || this._oldVerticalAcceleration == MudBuoyancy)
            {
                this._horizontalAcceleration = this.Horizontal;
                this._verticalAcceleration = this.Vertical;
            }
            else if (this._oldVerticalAcceleration != 0)
            {
                this._horizontalAcceleration = this.Horizontal;
                this._verticalAcceleration = 0;
            }
            else if (this._oldHorizontalAcceleration != 0)
            {
                this._horizontalAcceleration = 0;
                this._verticalAcceleration = this.Vertical;
            }
            else
            {
                this._horizontalAcceleration = this.Horizontal;
                this._verticalAcceleration = this.Vertical;
            }
            this._horizontalAcceleration = this._horizontalAcceleration * this.GetSpeedMultiplier();
            this._verticalAcceleration = this._verticalAcceleration * this.GetSpeedMultiplier();
            this._oldHorizontalAcceleration = this._oldHorizontalAcceleration * this._room.GravityMultiplier;
            this._oldVerticalAcceleration = this._oldVerticalAcceleration * this._room.GravityMultiplier;
            this.ModifierX = this._oldHorizontalAcceleration + this._horizontalAcceleration;

            this.ModifierY = this._oldVerticalAcceleration + this._verticalAcceleration;
            if (_speedX != 0 || _modifierX != 0)
            {
                _speedX = _speedX + _modifierX;
                _speedX = _speedX * _physicsBaseDrag;
                if (this._horizontalAcceleration == 0 && this._oldVerticalAcceleration != 0 ||
                    _speedX < 0 && this._horizontalAcceleration > 0 || _speedX > 0 && this._horizontalAcceleration < 0 ||
                    PhysicsUtils.IsClimbable(this._currentBlockId) && !this.IsGodOrMod)
                {
                    _speedX = _speedX * _noModifierDrag;
                }
                else if (this._currentBlockId == Block.Water && !this.IsGodOrMod)
                {
                    _speedX = _speedX * _waterDrag;
                }
                else if (this._currentBlockId == Block.Swamp && !this.IsGodOrMod)
                {
                    _speedX = _speedX * _mudDrag;
                }
                if (_speedX > 16)
                {
                    _speedX = 16;
                }
                else if (_speedX < -16)
                {
                    _speedX = -16;
                }
                else if (_speedX < 0.0001 && _speedX > -0.0001)
                {
                    _speedX = 0;
                }
            }
            if (_speedY != 0 || _modifierY != 0)
            {
                _speedY = _speedY + _modifierY;
                _speedY = _speedY * _physicsBaseDrag;
                if (this._verticalAcceleration == 0 && this._oldHorizontalAcceleration != 0 ||
                    _speedY < 0 && this._verticalAcceleration > 0 || _speedY > 0 && this._verticalAcceleration < 0 ||
                    PhysicsUtils.IsClimbable(this._currentBlockId) && !this.IsGodOrMod)
                {
                    _speedY = _speedY * _noModifierDrag;
                }
                else if (this._currentBlockId == Block.Water && !this.IsGodOrMod)
                {
                    _speedY = _speedY * _waterDrag;
                }
                else if (this._currentBlockId == Block.Swamp && !this.IsGodOrMod)
                {
                    _speedY = _speedY * _mudDrag;
                }
                if (_speedY > 16)
                {
                    _speedY = 16;
                }
                else if (_speedY < -16)
                {
                    _speedY = -16;
                }
                else if (_speedY < 0.0001 && _speedY > -0.0001)
                {
                    _speedY = 0;
                }
            }
            if (!this.IsGodOrMod)
            {
                switch (this._currentBlockId)
                {
                    case Block.BoostLeft:
                        _speedX = -BoostSpeed;
                        break;

                    case Block.BoostRight:
                        _speedX = BoostSpeed;
                        break;

                    case Block.BoostUp:
                        _speedY = -BoostSpeed;
                        break;

                    case Block.BoostDown:
                        _speedY = BoostSpeed;
                        break;
                }
            }

            this._reminderX = X % 1;
            this._currentSX = _speedX;
            this._reminderY = Y % 1;
            this._currentSY = _speedY;
            this._doneX = false;
            this._doneY = false;
            while (this._currentSX != 0 && !this._doneX || this._currentSY != 0 && !this._doneY)
            {
                this.ProcessPortals();
                this._oX = X;
                this._oY = Y;
                this._osX = this._currentSX;
                this._osY = this._currentSY;
                this.StepX();
                this.StepY();
            }

            if (this.BasePlayer.LevitationPotion)
            {
                this.UpdateThrust();
            }
            var imx = _speedX * 256;
            var imy = _speedY * 256;
            if (imx == 0 && this._currentBlockId != Block.Water && this._currentBlockId != Block.Swamp)
            {
                if (this._modifierX < 0.1 && this._modifierX > -0.1)
                {
                    this._tX = this.X % 16;
                    if (this._tX < 2)
                    {
                        if (this._tX < 0.2)
                        {
                            this.X = Math.Floor(this.X);
                        }
                        else
                        {
                            this.X = this.X - this._tX / 15;
                        }
                    }
                    else if (this._tX > 14)
                    {
                        if (this._tX > 15.8)
                        {
                            this.X = Math.Floor(this.X) + 1;
                        }
                        else
                        {
                            this.X = this.X + (this._tX - 14) / 15;
                        }
                    }
                }
            }
            if (imy == 0 && this._currentBlockId != Block.Water && this._currentBlockId != Block.Swamp)
            {
                if (this._modifierY < 0.1 && this._modifierY > -0.1)
                {
                    this._tY = this.Y % 16;
                    if (this._tY < 2)
                    {
                        if (this._tY < 0.2)
                        {
                            this.Y = Math.Floor(this.Y);
                        }
                        else
                        {
                            this.Y = this.Y - this._tY / 15;
                        }
                    }
                    else if (this._tY > 14)
                    {
                        if (this._tY > 15.8)
                        {
                            this.Y = Math.Floor(this.Y);
                            double loc3 = this.Y + 1;
                            this.Y = loc3;
                        }
                        else
                        {
                            this.Y = this.Y + (this._tY - 14) / 15;
                        }
                    }
                }
            }
        }

        private void UpdateThrust()
        {
            if (this._morY != 0)
            {
                this.SpeedY = this.SpeedY - this._currentThrust * (PhysicsJumpHeight / 2) * (this._morY * 0.5);
            }
            if (this._morX != 0)
            {
                this.SpeedX = this.SpeedX - this._currentThrust * (PhysicsJumpHeight / 2) * (this._morX * 0.5);
            }

            if (this._currentThrust > 0)
            {
                this._currentThrust = this._currentThrust - ThrustBurnOff;
            }
            else
            {
                this._currentThrust = 0;
            }
        }

        private bool HitTest(int param1, int param2)
        {
            return param1 >= X && param2 >= Y && param1 <= X + Size && param2 <= Y + Size;
        }
    }
}