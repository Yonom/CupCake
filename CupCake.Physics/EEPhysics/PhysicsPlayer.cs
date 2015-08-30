using System;
using System.Collections;
using System.Collections.Generic;
using CupCake.Core;

namespace CupCake.Physics.EEPhysics
{
    public class PhysicsPlayer
    {
        internal PhysicsWorld HostWorld { get; set; }
        internal const int Width = 16;
        internal const int Height = 16;
        /// <summary>
        /// The precise player X position.
        /// </summary>
        public double X { get; internal set; }
        /// <summary>
        /// The precise player Y position.
        /// </summary>
        public double Y { get; internal set; }
        public int Horizontal { get; internal set; }
        public int Vertical { get; internal set; }
        private int current, currentBelow;
        private double oldX, oldY;
        private double speedX = 0;
        private double speedY = 0;
        private double modifierX = 0;
        private double modifierY = 0;
        private int gravity;
        private double mox, moy;
        private int morx, mory;
        private int pastx, pasty;
        internal int overlapy;
        private double mx, my;
        private int tx = -1;
        private int ty = -1;
        private Queue<int> queue = new Queue<int>();
        private Queue<int> tileQueue = new Queue<int>();
        private bool IsInvulnerable;
        private bool donex, doney;
        private int oh, ov;
        internal BitArray Switches { get; set; }
        internal int deaths = 0;
        private const double portalMultiplier = 1.42;
        private bool hasLastPortal = false;
        private Point lastPortal;
        private List<Point> gotCoins = new List<Point>();
        private List<Point> gotBlueCoins = new List<Point>();
        private bool isOnFire;
        private int onFireDeath = 200;
        private float deathOffset = 0;

        private const double maxThrust = 0.2;

        public bool HasLevitation { get; internal set; }
        public bool IsThrusting { get; internal set; }
        private double currentThrust = maxThrust;
        private double thrustBurnOff = 0.01;
        public bool JumpBoostEffect { get; internal set; }
        public bool SpeedBoostEffect { get; internal set; }
        public bool CursedEffect { get; internal set; }
        public bool Zombie { get; internal set; }

        /// <summary>Also includes moderator and guardian mode.</summary>
        public bool InGodMode { get; internal set; }
        public bool IsDead { get; internal set; }
        public int Team { get; internal set; }
        public bool HasChat { get; internal set; }
        public bool HasCrown { get; internal set; }
        public bool IsMe { get; internal set; }

        private long lastJump;
        private bool spacejustdown = false;
        public bool SpaceDown { get; set; }

        internal double GravityMultiplier { get { return this.HostWorld.WorldGravity; } }
        internal double SpeedMultiplier
        {
            get
            {
                double d = 1.0;
                if (this.Zombie)
                {
                    d *= 1.2;
                }
                if (this.SpeedBoostEffect)
                {
                    d *= 1.5;
                }
                return d;
            }
        }
        public double SpeedX { get { return this.speedX * PhysicsConfig.VariableMultiplier; } internal set { this.speedX = value / PhysicsConfig.VariableMultiplier; } }
        public double SpeedY { get { return this.speedY * PhysicsConfig.VariableMultiplier; } internal set { this.speedY = value / PhysicsConfig.VariableMultiplier; } }
        public double ModifierX { get { return this.modifierX * PhysicsConfig.VariableMultiplier; } internal set { this.modifierX = value / PhysicsConfig.VariableMultiplier; } }
        public double ModifierY { get { return this.modifierY * PhysicsConfig.VariableMultiplier; } internal set { this.modifierY = value / PhysicsConfig.VariableMultiplier; } }
        internal double JumpMultiplier
        {
            get
            {
                double d = 1.0;
                if (this.JumpBoostEffect)
                {
                    d *= 1.3;
                }
                if (this.Zombie)
                {
                    d *= 0.75;
                }
                return d;
            }
        }

        public int LastCheckpointX { get; private set; }
        public int LastCheckpointY { get; private set; }

        /// <summary>
        /// The player ID in PlayerIO Messages
        /// </summary>
        public int ID { get; private set; }
        /// <summary>
        /// Player name in the world.
        /// </summary>
        public string Name { get; protected set; }
        public int Coins { get; set; }
        public int BlueCoins { get; set; }
        public bool IsClubMember { get; set; }


        public delegate void PlayerEvent(PlayerEventArgs e);

        private Dictionary<int, PlayerEvent> blockIdEvents = new Dictionary<int, PlayerEvent>();
        private Dictionary<int, PlayerEvent> bgblockIdEvents = new Dictionary<int, PlayerEvent>();
        private Dictionary<int, PlayerEvent> touchBlockEvents = new Dictionary<int, PlayerEvent>();
        private List<Point> touchedPoints = new List<Point>();

        public event PlayerEvent OnHitCrown = delegate { };

        /// <summary>
        /// Note: This will be called every time player hits coin, even if the coin is already got by that player. If you want to get only first time coin is hit, use event OnGetCoin.
        /// </summary>
        public event PlayerEvent OnHitCoin = delegate { };
        /// <summary>
        /// Will be called only when player hits a coin first time. After first time, only event OnHitCoin will be called.
        /// </summary>
        public event PlayerEvent OnGetCoin = delegate { };

        /// <summary>
        /// Note: This will be called every time player hits blue coin, even if the coin is already got by that player. If you want to get only first time coin is hit, use event OnGetBlueCoin.
        /// </summary>
        public event PlayerEvent OnHitBlueCoin = delegate { };
        /// <summary>
        /// Will be called only when player hits a blue coin first time. After first time, only event OnHitBlueCoin will be called.
        /// </summary>
        public event PlayerEvent OnGetBlueCoin = delegate { };

        /// <summary>
        /// Includes invisible portals.
        /// </summary>
        public event PlayerEvent OnHitPortal = delegate { };

        public event PlayerEvent OnHitRedKey = delegate { };
        public event PlayerEvent OnHitBlueKey = delegate { };
        public event PlayerEvent OnHitGreenKey = delegate { };
        public event PlayerEvent OnHitCyanKey = delegate { };
        public event PlayerEvent OnHitMagentaKey = delegate { };
        public event PlayerEvent OnHitYellowKey = delegate { };

        public event PlayerEvent OnHitPiano = delegate { };
        public event PlayerEvent OnHitDrum = delegate { };
        public event PlayerEvent OnHitSwitch = delegate { };
        public event PlayerEvent OnHitDiamond = delegate { };
        public event PlayerEvent OnHitCake = delegate { };

        public event PlayerEvent OnHitCompleteLevelBrick = delegate { };
        public event PlayerEvent OnHitCheckpoint = delegate { };

        public event PlayerEvent OnDie = delegate { };


        public PhysicsPlayer(int id, string name)
        {
            this.ID = id;
            this.Name = name;
            this.X = 16;
            this.Y = 16;
            this.gravity = (int)PhysicsConfig.Gravity;
            this.Switches = new BitArray(100);
        }

        internal void Tick()
        {
            double reminderX;
            double currentSX;
            double osx;
            double ox;
            double tx;
            double reminderY;
            double currentSY;
            double osy;
            double oy;
            double ty;
            bool spacedown = this.SpaceDown;

            if (this.IsDead)
            {
                if (this.IsMe && this.HostWorld.Connected)
                {
                    this.deathOffset += 0.3f;
                    if (this.deathOffset >= 16.0f)
                    {
                        this.HostWorld.Connection.Send("death");
                        this.deathOffset = 0;
                    }
                }
            }

            int cx = ((int)(this.X + 8) >> 4);
            int cy = ((int)(this.Y + 8) >> 4);

            int delayed;
            if (this.queue.Count > 0)
                delayed = this.queue.Dequeue();
            else
                delayed = 0;
            this.current = this.HostWorld.GetBlock(0, cx, cy);
            if (this.tx != -1)
            {
                this.UpdateTeamDoors(this.tx, this.ty);
            }
            this.currentBelow = 0;
            if (this.current == 1 || this.current == 411)
            {
                this.currentBelow = this.HostWorld.GetBlock(0, cx - 1, cy);
            }
            else if (this.current == 2 || this.current == 412)
            {
                this.currentBelow = this.HostWorld.GetBlock(0, cx, cy - 1);
            }
            else if (this.current == 3 || this.current == 413)
            {
                this.currentBelow = this.HostWorld.GetBlock(0, cx + 1, cy);
            }
            else
            {
                this.currentBelow = this.HostWorld.GetBlock(0, cx, cy + 1);
            }

            this.queue.Enqueue(this.current);
            if (this.current == 4 || this.current == 414 || ItemId.isClimbable(this.current))
            {
                delayed = this.queue.Dequeue();
                this.queue.Enqueue(this.current);
            }
            // not needed, client side only
            /*while (tileQueue.Count > 0)
            {
                UpdatePurpleSwitches(tileQueue.Dequeue());
            }*/

            if (this.isOnFire && !this.IsInvulnerable)
            {
                if (this.onFireDeath <= 0)
                {
                    this.onFireDeath = 200;
                    this.KillPlayer();
                }
                else
                {
                    this.onFireDeath--;
                }
            }
            else
            {
                this.onFireDeath = 200;
            }

            if (this.IsDead)
            {
                this.Horizontal = 0;
                this.Vertical = 0;
                spacedown = false;
                this.spacejustdown = false;
            }

            bool isGodMode = this.InGodMode;
            if (this.InGodMode)
            {
                this.morx = 0;
                this.mory = 0;
                this.mox = 0;
                this.moy = 0;
            }
            else
            {
                if (ItemId.isClimbable(this.current))
                {
                    this.morx = 0;
                    this.mory = 0;
                }
                else
                {
                    switch (this.current)
                    {
                        case 1:
                        case ItemId.InvisibleLeftArrow:
                            this.morx = -((int)this.gravity);
                            this.mory = 0;
                            break;
                        case 2:
                        case ItemId.InvisibleUpArrow:
                            this.morx = 0;
                            this.mory = -((int)this.gravity);
                            break;
                        case 3:
                        case ItemId.InvisibleRightArrow:
                            this.morx = (int)this.gravity;
                            this.mory = 0;
                            break;
                        case ItemId.SpeedLeft:
                        case ItemId.SpeedRight:
                        case ItemId.SpeedUp:
                        case ItemId.SpeedDown:
                        case ItemId.InvisibleDot:
                        case 4:
                            this.morx = 0;
                            this.mory = 0;
                            break;
                        case ItemId.Water:
                            this.morx = 0;
                            this.mory = (int)PhysicsConfig.WaterBuoyancy;
                            if (this.isOnFire)
                            {
                                this.isOnFire = false;
                            }
                            break;
                        case ItemId.Mud:
                            this.morx = 0;
                            this.mory = (int)PhysicsConfig.MudBuoyancy;
                            if (this.isOnFire)
                            {
                                this.isOnFire = false;
                            }
                            break;
                        case ItemId.Lava:
                            this.morx = 0;
                            this.mory = (int)PhysicsConfig.LavaBuoyancy;
                            if (!this.isOnFire && !this.IsInvulnerable)
                            {
                                this.isOnFire = true;
                            }
                            break;
                        case ItemId.Fire:
                        case ItemId.Spike:
                            this.morx = 0;
                            this.mory = this.gravity;
                            if (!this.IsDead && !this.IsInvulnerable)
                            {
                                this.KillPlayer();
                            }
                            break;
                        case ItemId.EffectProtection:
                            this.morx = 0;
                            this.mory = this.gravity;
                            if (this.HostWorld.GetOnStatus(cx, cy) && this.isOnFire)
                            {
                                this.isOnFire = false;
                            }
                            break;
                        default:
                            this.morx = 0;
                            this.mory = this.gravity;
                            break;
                    }
                }

                if (ItemId.isClimbable(delayed))
                {
                    this.mox = 0;
                    this.moy = 0;
                }
                else
                {
                    switch (delayed)
                    {
                        case 1:
                        case ItemId.InvisibleLeftArrow:
                            this.mox = -this.gravity;
                            this.moy = 0;
                            break;
                        case 2:
                        case ItemId.InvisibleUpArrow:
                            this.mox = 0;
                            this.moy = -this.gravity;
                            break;
                        case 3:
                        case ItemId.InvisibleRightArrow:
                            this.mox = this.gravity;
                            this.moy = 0;
                            break;
                        case ItemId.SpeedLeft:
                        case ItemId.SpeedRight:
                        case ItemId.SpeedUp:
                        case ItemId.SpeedDown:
                        case ItemId.InvisibleDot:
                        case 4:
                            this.mox = 0;
                            this.moy = 0;
                            break;
                        case ItemId.Water:
                            this.mox = 0;
                            this.moy = PhysicsConfig.WaterBuoyancy;
                            break;
                        case ItemId.Mud:
                            this.mox = 0;
                            this.moy = PhysicsConfig.MudBuoyancy;
                            break;
                        case ItemId.Lava:
                            this.mox = 0;
                            this.moy = PhysicsConfig.LavaBuoyancy;
                            break;
                        default:
                            this.mox = 0;
                            this.moy = this.gravity;
                            break;
                    }
                }
            }

            if (this.moy == PhysicsConfig.WaterBuoyancy || this.moy == PhysicsConfig.MudBuoyancy || this.moy == PhysicsConfig.LavaBuoyancy)
            {
                this.mx = this.Horizontal;
                this.my = this.Vertical;
            }
            else if (this.moy != 0)
            {
                this.mx = this.Horizontal;
                this.my = 0;
            }
            else if (this.mox != 0)
            {
                this.mx = 0;
                this.my = this.Vertical;
            }
            else
            {
                this.mx = this.Horizontal;
                this.my = this.Vertical;
            }

            this.mx *= this.SpeedMultiplier;
            this.my *= this.SpeedMultiplier;
            this.mox *= this.GravityMultiplier;
            this.moy *= this.GravityMultiplier;

            this.ModifierX = (this.mox + this.mx);
            this.ModifierY = (this.moy + this.my);

            if (!this.DoubleIsEqual(this.speedX, 0) || !this.DoubleIsEqual(this.modifierX, 0))
            {
                this.speedX = (this.speedX + this.modifierX);
                this.speedX = (this.speedX * PhysicsConfig.BaseDrag);
                if (!isGodMode)
                {
                    if ((this.mx == 0 && this.moy != 0) || (this.speedX < 0 && this.mx > 0) || (this.speedX > 0 && this.mx < 0) || ItemId.isClimbable(this.current))
                    {
                        this.speedX = (this.speedX * PhysicsConfig.NoModifierDrag);
                    }
                    else if (this.current == ItemId.Water)
                    {
                        this.speedX = (this.speedX * PhysicsConfig.WaterDrag);
                    }
                    else if (this.current == ItemId.Mud)
                    {
                        this.speedX = (this.speedX * PhysicsConfig.MudDrag);
                    }
                    else if (this.current == ItemId.Lava)
                    {
                        this.speedX = (this.speedX * PhysicsConfig.LavaDrag);
                    }
                }

                if (this.speedX > 16)
                {
                    this.speedX = 16;
                }
                else if (this.speedX < -16)
                {
                    this.speedX = -16;
                }
                else if (this.speedX < 0.0001 && this.speedX > -0.0001)
                {
                    this.speedX = 0;
                }
            }
            if (!this.DoubleIsEqual(this.speedY, 0) || !this.DoubleIsEqual(this.modifierY, 0))
            {
                this.speedY = (this.speedY + this.modifierY);
                this.speedY = (this.speedY * PhysicsConfig.BaseDrag);
                if (!isGodMode)
                {
                    if ((this.my == 0 && this.mox != 0) || (this.speedY < 0 && this.my > 0) || (this.speedY > 0 && this.my < 0) || ItemId.isClimbable(this.current))
                    {
                        this.speedY = (this.speedY * PhysicsConfig.NoModifierDrag);
                    }
                    else if (this.current == ItemId.Water)
                    {
                        this.speedY = (this.speedY * PhysicsConfig.WaterDrag);
                    }
                    else if (this.current == ItemId.Mud)
                    {
                        this.speedY = (this.speedY * PhysicsConfig.MudDrag);
                    }
                    else if (this.current == ItemId.Lava)
                    {
                        this.speedY = (this.speedY * PhysicsConfig.LavaDrag);
                    }
                }

                if (this.speedY > 16)
                {
                    this.speedY = 16;
                }
                else if (this.speedY < -16)
                {
                    this.speedY = -16;
                }
                else if (this.speedY < 0.0001 && this.speedY > -0.0001)
                {
                    this.speedY = 0;
                }
            }

            if (!isGodMode)
            {
                switch (this.current)
                {
                    case ItemId.SpeedLeft:
                        this.speedX = -PhysicsConfig.Boost;
                        break;
                    case ItemId.SpeedRight:
                        this.speedX = PhysicsConfig.Boost;
                        break;
                    case ItemId.SpeedUp:
                        this.speedY = -PhysicsConfig.Boost;
                        break;
                    case ItemId.SpeedDown:
                        this.speedY = PhysicsConfig.Boost;
                        break;
                }
            }

            reminderX = this.X % 1;
            currentSX = this.speedX;
            reminderY = this.Y % 1;
            currentSY = this.speedY;
            this.donex = false;
            this.doney = false;

            while ((currentSX != 0 && !this.donex) || (currentSY != 0 && !this.doney))
            {
                #region processPortals()
                this.current = this.HostWorld.GetBlock(cx, cy);
                if (!isGodMode && (this.current == ItemId.Portal || this.current == ItemId.PortalInvisible))
                {
                    if (!this.hasLastPortal)
                    {
                        this.OnHitPortal(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                        this.lastPortal = new Point(cx, cy);
                        this.hasLastPortal = true;
                        int[] data = this.HostWorld.GetBlockData(cx, cy);
                        if (data != null && data.Length == 3)
                        {
                            Point portalPoint;
                            if (data[2] != data[1] && // target != itself
                                this.HostWorld.TryGetPortalById(data[2], out portalPoint))
                            {
                                int[] data2 = this.HostWorld.GetBlockData(this.lastPortal.X, this.lastPortal.Y);
                                int[] data3 = this.HostWorld.GetBlockData(portalPoint.X, portalPoint.Y);
                                if (data2 != null && data2.Length == 3 &&
                                    data3 != null && data3.Length == 3)
                                {
                                    int rot1 = data2[0];
                                    int rot2 = data3[0];
                                    if (rot1 < rot2)
                                    {
                                        rot1 += 4;
                                    }
                                    switch (rot1 - rot2)
                                    {
                                        case 1:
                                            this.SpeedX = this.SpeedY * portalMultiplier;
                                            this.SpeedY = -this.SpeedX * portalMultiplier;
                                            this.ModifierX = this.ModifierY * portalMultiplier;
                                            this.ModifierY = -this.ModifierX * portalMultiplier;
                                            reminderY = -reminderY;
                                            currentSY = -currentSY;
                                            break;
                                        case 2:
                                            this.SpeedX = -this.SpeedX * portalMultiplier;
                                            this.SpeedY = -this.SpeedY * portalMultiplier;
                                            this.ModifierX = -this.ModifierX * portalMultiplier;
                                            this.ModifierY = -this.ModifierY * portalMultiplier;
                                            reminderY = -reminderY;
                                            currentSY = -currentSY;
                                            reminderX = -reminderX;
                                            currentSX = -currentSX;
                                            break;
                                        case 3:
                                            this.SpeedX = -this.SpeedY * portalMultiplier;
                                            this.SpeedY = this.SpeedX * portalMultiplier;
                                            this.ModifierX = -this.ModifierY * portalMultiplier;
                                            this.ModifierY = this.ModifierX * portalMultiplier;
                                            reminderX = -reminderX;
                                            currentSX = -currentSX;
                                            break;
                                    }
                                    this.X = portalPoint.X * 16;
                                    this.Y = portalPoint.Y * 16;
                                    this.lastPortal = portalPoint;
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.hasLastPortal = false;
                }
                #endregion

                ox = this.X;
                oy = this.Y;
                osx = currentSX;
                osy = currentSY;

                #region stepX()
                if (currentSX > 0)
                {
                    if ((currentSX + reminderX) >= 1)
                    {
                        this.X += 1 - reminderX;
                        this.X = Math.Floor(this.X);
                        currentSX -= 1 - reminderX;
                        reminderX = 0;
                    }
                    else
                    {
                        this.X += currentSX;
                        currentSX = 0;
                    }
                }
                else
                {
                    if (currentSX < 0)
                    {
                        if (!this.DoubleIsEqual(reminderX, 0) && (reminderX + currentSX) < 0)
                        {
                            currentSX += reminderX;
                            this.X -= reminderX;
                            this.X = Math.Floor(this.X);
                            reminderX = 1;
                        }
                        else
                        {
                            this.X += currentSX;
                            currentSX = 0;
                        }
                    }
                }
                if (this.HostWorld.Overlaps(this))
                {
                    this.X = ox;
                    this.speedX = 0;
                    currentSX = osx;
                    this.donex = true;
                }
                #endregion

                #region stepY()
                if (currentSY > 0)
                {
                    if ((currentSY + reminderY) >= 1)
                    {
                        this.Y += 1 - reminderY;
                        this.Y = Math.Floor(this.Y);
                        currentSY -= 1 - reminderY;
                        reminderY = 0;
                    }
                    else
                    {
                        this.Y += currentSY;
                        currentSY = 0;
                    }
                }
                else
                {
                    if (currentSY < 0)
                    {
                        if (!this.DoubleIsEqual(reminderY, 0) && (reminderY + currentSY) < 0)
                        {
                            this.Y -= reminderY;
                            this.Y = Math.Floor(this.Y);
                            currentSY += reminderY;
                            reminderY = 1;
                        }
                        else
                        {
                            this.Y += currentSY;
                            currentSY = 0;
                        }
                    }
                }
                if (this.HostWorld.Overlaps(this))
                {
                    this.Y = oy;
                    this.speedY = 0;
                    currentSY = osy;
                    this.doney = true;
                }
                #endregion
            }


            if (!this.IsDead)
            {
                if (this.IsMe)
                {
                    int mod = 1;
                    bool injump = false;
                    bool changed = false;
                    if (this.spacejustdown)
                    {
                        this.lastJump = -this.HostWorld.sw.ElapsedMilliseconds;
                        injump = true;
                        mod = -1;
                    }
                    if (this.SpaceDown)
                    {
                        if (this.HasLevitation)
                        {
                            if (this.IsThrusting)
                            {
                                changed = true;
                            }
                            this.IsThrusting = true;
                            this.ApplyThrust();
                        }
                        else if (this.lastJump < 0)
                        {
                            if (this.HostWorld.sw.ElapsedMilliseconds + this.lastJump > 750)
                            {
                                injump = true;
                            }
                        }
                        else if (this.HostWorld.sw.ElapsedMilliseconds - this.lastJump > 150)
                        {
                            injump = true;
                        }
                    }
                    else if (this.HasLevitation)
                    {
                        if (this.IsThrusting)
                        {
                            changed = true;
                        }
                        this.IsThrusting = true;
                    }
                    if (injump && !this.HasLevitation)
                    {
                        if (this.SpeedX == 0 && this.morx != 0 && this.mox != 0 && (this.X % 16 == 0 || this.X % 8 == 0))
                        {
                            this.SpeedX -= this.morx * PhysicsConfig.JumpHeight * this.JumpMultiplier;
                            changed = true;
                            this.lastJump = this.HostWorld.sw.ElapsedMilliseconds * mod;
                        }
                        if (this.SpeedY == 0 && this.mory != 0 && this.moy != 0 && (this.Y % 16 == 0 || this.Y % 8 == 0))
                        {
                            this.SpeedY -= this.mory * PhysicsConfig.JumpHeight * this.JumpMultiplier;
                            changed = true;
                            this.lastJump = this.HostWorld.sw.ElapsedMilliseconds * mod;
                        }
                    }
                    if (changed || this.oh != this.Horizontal || this.ov != this.Vertical)
                    {
                        this.oh = this.Horizontal;
                        this.ov = this.Vertical;
                    }
                }
                if (this.pastx != cx || this.pasty != cy)
                {
                    PlayerEvent e;
                    if (this.blockIdEvents.Count != 0 && this.blockIdEvents.TryGetValue(this.current, out e))
                    {
                        e(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                    }
                    if (this.bgblockIdEvents.Count != 0 && this.bgblockIdEvents.TryGetValue(this.HostWorld.GetBlock(1, cx, cy), out e))
                    {
                        e(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                    }

                    // Might remove specific events soon, because you can make them now with void AddBlockEvent. (except OnGetCoin and OnGetBlueCoin)
                    switch (this.current)
                    {
                        case 100:   //coin
                            this.OnHitCoin(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            for (int i = 0; i < this.gotCoins.Count; i++)
                            {
                                if (this.gotCoins[i].X == cx && this.gotCoins[i].Y == cy)
                                {
                                    goto found;
                                }
                            }
                            this.OnGetCoin(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            this.gotCoins.Add(new Point(cx, cy));
                            if (this.IsMe && this.HostWorld.Connected)
                            {
                                this.HostWorld.Connection.Send("c", this.Coins, this.BlueCoins, cx, cy);
                            }
                        found:
                            break;
                        case 101:   // bluecoin
                            this.OnHitBlueCoin(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            for (int i = 0; i < this.gotBlueCoins.Count; i++)
                            {
                                if (this.gotBlueCoins[i].X == cx && this.gotBlueCoins[i].Y == cy)
                                {
                                    goto found2;
                                }
                            }
                            this.OnGetBlueCoin(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            this.gotBlueCoins.Add(new Point(cx, cy));
                            if (this.IsMe && this.HostWorld.Connected)
                            {
                                this.HostWorld.Connection.Send("c", this.Coins, this.BlueCoins, cx, cy);
                            }
                        found2:
                            break;
                        case 5:
                            // crown
                            this.OnHitCrown(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            if (this.IsMe && this.HostWorld.Connected && !this.InGodMode && !this.HasCrown)
                            {
                                this.HostWorld.Connection.Send(this.HostWorld.WorldKey + "k", cx, cy);
                            }
                            break;
                        case 6:
                            // red key
                            this.OnHitRedKey(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            if (this.IsMe && this.HostWorld.Connected && !this.InGodMode)
                            {
                                this.HostWorld.Connection.Send(this.HostWorld.WorldKey + "r", cx, cy);
                            }
                            break;
                        case 7:
                            // green key
                            this.OnHitGreenKey(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            if (this.IsMe && this.HostWorld.Connected && !this.InGodMode)
                            {
                                this.HostWorld.Connection.Send(this.HostWorld.WorldKey + "g", cx, cy);
                            }
                            break;
                        case 8:
                            // blue key
                            this.OnHitBlueKey(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            if (this.IsMe && this.HostWorld.Connected && !this.InGodMode)
                            {
                                this.HostWorld.Connection.Send(this.HostWorld.WorldKey + "b", cx, cy);
                            }
                            break;
                        case ItemId.CyanKey:
                            this.OnHitCyanKey(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            if (this.IsMe && this.HostWorld.Connected && !this.InGodMode)
                            {
                                this.HostWorld.Connection.Send(this.HostWorld.WorldKey + "c", cx, cy);
                            }
                            break;
                        case ItemId.MagentaKey:
                            this.OnHitMagentaKey(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            if (this.IsMe && this.HostWorld.Connected && !this.InGodMode)
                            {
                                this.HostWorld.Connection.Send(this.HostWorld.WorldKey + "m", cx, cy);
                            }
                            break;
                        case ItemId.YellowKey:
                            this.OnHitYellowKey(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            if (this.IsMe && this.HostWorld.Connected && !this.InGodMode)
                            {
                                this.HostWorld.Connection.Send(this.HostWorld.WorldKey + "y", cx, cy);
                            }
                            break;
                        case ItemId.SwitchPurple:
                            int sid = this.HostWorld.GetBlockData(cx, cy)[0];
                            this.UpdatePurpleSwitches(sid);
                            this.OnHitSwitch(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            break;
                        case ItemId.Piano:
                            this.OnHitPiano(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            break;
                        case ItemId.Drum:
                            this.OnHitDrum(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            break;
                        case ItemId.Diamond:
                            this.OnHitDiamond(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            if (this.IsMe && this.HostWorld.Connected && !this.InGodMode)
                            {
                                this.HostWorld.Connection.Send("diamondtouch", cx, cy);
                            }
                            break;
                        case ItemId.Cake:
                            this.OnHitCake(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            if (this.IsMe && this.HostWorld.Connected && !this.InGodMode)
                            {
                                this.HostWorld.Connection.Send("caketouch", cx, cy);
                            }
                            break;
                        case ItemId.Hologram:
                            if (this.IsMe && this.HostWorld.Connected && !this.InGodMode)
                            {
                                this.HostWorld.Connection.Send("hologramtouch", cx, cy);
                            }
                            break;
                        case ItemId.Checkpoint:
                            if (!isGodMode)
                            {
                                this.LastCheckpointX = cx;
                                this.LastCheckpointY = cy;
                                this.OnHitCheckpoint(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                                if (this.IsMe && this.HostWorld.Connected)
                                {
                                    this.HostWorld.Connection.Send("checkpoint", cx, cy);
                                }
                            }
                            break;
                        case ItemId.BrickComplete:
                            this.OnHitCompleteLevelBrick(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            if (this.IsMe && this.HostWorld.Connected && !this.InGodMode)
                            {
                                this.HostWorld.Connection.Send("levelcomplete", cx, cy);
                            }
                            break;
                        case ItemId.EffectProtection:
                            // TODO
                            break;
                    }
                    this.pastx = cx;
                    this.pasty = cy;
                }
                if (this.touchBlockEvents.Count > 0)
                {
                    PlayerEvent e;
                    Point p;
                    if (this.oldX != this.X || this.oldY != this.Y)
                    {
                        for (int i = 0; i < this.touchedPoints.Count; i++)
                        {
                            if (this.touchedPoints[i].Y == cy)
                            {
                                if (this.X % 16 == 0 && (this.touchedPoints[i].X == cx - 1 || this.touchedPoints[i].X == cx + 1) && this.touchedPoints[i].Y == cy)
                                {

                                }
                                else
                                {
                                    this.touchedPoints.RemoveAt(i--);
                                }
                            }
                            else if (this.touchedPoints[i].X == cx)
                            {
                                if (this.Y % 16 == 0 && (this.touchedPoints[i].Y == cy - 1 || this.touchedPoints[i].Y == cy + 1) && this.touchedPoints[i].X == cx)
                                {

                                }
                                else
                                {
                                    this.touchedPoints.RemoveAt(i--);
                                }
                            }
                            else
                            {
                                this.touchedPoints.RemoveAt(i--);
                            }
                        }
                        if (this.X % 16 == 0)
                        {
                            p = new Point(cx - 1, cy);
                            if (ItemId.isSolid(this.HostWorld.GetBlock(0, p.X, p.Y)) && this.touchBlockEvents.TryGetValue(this.HostWorld.GetBlock(0, p.X, p.Y), out e))
                            {
                                if (!this.touchedPoints.Contains(p))
                                {
                                    this.touchedPoints.Add(p);
                                    e(new PlayerEventArgs() { Player = this, BlockX = p.X, BlockY = p.Y });
                                }
                            }
                            p = new Point(cx + 1, cy);
                            if (ItemId.isSolid(this.HostWorld.GetBlock(0, p.X, p.Y)) && this.touchBlockEvents.TryGetValue(this.HostWorld.GetBlock(0, p.X, p.Y), out e))
                            {
                                if (!this.touchedPoints.Contains(p))
                                {
                                    this.touchedPoints.Add(p);
                                    e(new PlayerEventArgs() { Player = this, BlockX = p.X, BlockY = p.Y });
                                }
                            }
                        }
                        if (this.Y % 16 == 0)
                        {
                            p = new Point(cx, cy - 1);
                            if (ItemId.isSolid(this.HostWorld.GetBlock(0, p.X, p.Y)) && this.touchBlockEvents.TryGetValue(this.HostWorld.GetBlock(0, p.X, p.Y), out e))
                            {
                                if (!this.touchedPoints.Contains(p))
                                {
                                    this.touchedPoints.Add(p);
                                    e(new PlayerEventArgs() { Player = this, BlockX = p.X, BlockY = p.Y });
                                }
                            }
                            p = new Point(cx, cy + 1);
                            if (ItemId.isSolid(this.HostWorld.GetBlock(0, p.X, p.Y)) && this.touchBlockEvents.TryGetValue(this.HostWorld.GetBlock(0, p.X, p.Y), out e))
                            {
                                if (!this.touchedPoints.Contains(p))
                                {
                                    this.touchedPoints.Add(p);
                                    e(new PlayerEventArgs() { Player = this, BlockX = p.X, BlockY = p.Y });
                                }
                            }
                        }
                    }
                }
            }

            if (this.HasLevitation)
            {
                this.UpdateThrust();
            }

            int imx = ((int)this.speedX << 8);
            int imy = ((int)this.speedX << 8);
            if (imx != 0 || ((this.current == ItemId.Water || this.current == ItemId.Mud || this.current == ItemId.Lava) && !this.InGodMode))
            {

            }
            else if (this.modifierY < 0.1 && this.modifierX > -0.1)
            {
                tx = (this.X % 16);
                if (tx < 2)
                {
                    if (tx < 0.2)
                    {
                        this.X = Math.Floor(this.X);
                    }
                    else
                    {
                        this.X -= tx / 15;
                    }
                }
                else
                {
                    if (tx > 14)
                    {
                        if (tx > 15.8)
                        {
                            this.X = Math.Floor(this.X);
                            this.X++;
                        }
                        else
                        {
                            this.X += (tx - 14) / 15;
                        }
                    }
                }
            }
            if (imx != 0 || ((this.current == ItemId.Water || this.current == ItemId.Mud || this.current == ItemId.Lava) && !this.InGodMode))
            {

            }
            else if (this.modifierY < 0.1 && this.modifierY > -0.1)
            {
                ty = (this.Y % 16);
                if (ty < 2)
                {
                    if (ty < 0.2)
                    {
                        this.Y = Math.Floor(this.Y);
                    }
                    else
                    {
                        this.Y -= ty / 15;
                    }
                }
                else
                {
                    if (ty > 14)
                    {
                        if (ty > 15.8)
                        {
                            this.Y = Math.Floor(this.Y);
                            this.Y++;
                        }
                        else
                        {
                            this.Y += (ty - 14) / 15;
                        }
                    }
                }
            }

            this.oldX = this.X;
            this.oldY = this.Y;
        }

        /// <summary>
        /// Set horizontal movement direction of the bot. Allowed only for the bot player. Allowed only for the bot player. Also you have to initialize PhysicsWorld with the PlayerIO Connection.
        /// </summary>
        /// <param name="horizontal">-1 = left, 1 = right</param>
        public void SetHorizontal(int horizontal)
        {
            if (!this.IsMe)
            {
                throw new Exception("Allowed only for the bot player.");
            }
            if (!this.HostWorld.Connected)
            {
                throw new Exception("EEPhysics needs connection to move the bot. Make sure you initialized PhysicsWorld with PlayerIO Connection.");
            }
            this.Horizontal = horizontal;
        }
        /// <summary>
        /// Set horizontal movement direction of the bot. Allowed only for the bot player. Also you have to initialize PhysicsWorld with the PlayerIO Connection.
        /// </summary>
        /// <param name="vertical">-1 = up, 1 = down</param>
        public void SetVertical(int vertical)
        {
            if (!this.IsMe)
            {
                throw new Exception("Allowed only for the bot player.");
            }
            if (!this.HostWorld.Connected)
            {
                throw new Exception("EEPhysics needs connection to move the bot. Make sure you initialized PhysicsWorld with PlayerIO Connection.");
            }
            this.Vertical = vertical;
        }

        /// <summary>
        /// Makes PhysicsPlayer raise event when player moves inside blockId block. Event is not raised every tick, but only at when player first touches the block.
        /// (Touching doesn't count! Only the block that is at center of player is checked, no multiple blocks at same time.)
        /// </summary>
        /// <param name="blockId">Block ID to check for.</param>
        /// <param name="e">Method which is run when event occurs.</param>
        public void AddBlockEvent(int blockId, PlayerEvent e)
        {
            if (!ItemId.IsBackground(blockId))
                this.blockIdEvents[blockId] = e;
            else
                this.bgblockIdEvents[blockId] = e;
        }
        /// <returns>Whether there's block event with specified blockId.</returns>
        public bool HasBlockEvent(int blockId)
        {
            if (!ItemId.IsBackground(blockId))
                return this.blockIdEvents.ContainsKey(blockId);
            else
                return this.bgblockIdEvents.ContainsKey(blockId);
        }
        /// <summary>
        /// Removes block event added with AddBlockEvent with specified blockId.
        /// </summary>
        public void RemoveBlockEvent(int blockId)
        {
            if (!ItemId.IsBackground(blockId))
                this.blockIdEvents.Remove(blockId);
            else
                this.bgblockIdEvents.Remove(blockId);
        }

        public bool GetSwitchState(int switchId)
        {
            return this.Switches[switchId];
        }

        /// <summary>
        /// Makes PhysicsPlayer raise event when player touches blockId block (doesn't need to overlap). Event is not raised every tick, but at least when player first touches the block. It is possible that it's raised multiple times for same block in some cases.
        /// </summary>
        /// <param name="blockId">Block ID to check for.</param>
        /// <param name="e">Method which is run when event occurs.</param>
        public void AddBlockTouchEvent(int blockId, PlayerEvent e)
        {
            this.touchBlockEvents[blockId] = e;
        }
        /// <returns>Whether there's block event with specified blockId.</returns>
        public bool HasBlockTouchEvent(int blockId)
        {
            return this.touchBlockEvents.ContainsKey(blockId);
        }
        /// <summary>
        /// Removes block event added with AddBlockTouchEvent with specified blockId.
        /// </summary>
        public void RemoveBlockTouchEvent(int blockId)
        {
            this.touchBlockEvents.Remove(blockId);
        }

        /// <returns>True if player overlaps block at x,y.</returns>
        public bool OverlapsTile(int tx, int ty)
        {
            int xx = tx * 16;
            int yy = ty * 16;
            return ((this.X > xx - 16 && this.X <= xx + 16) && (this.Y > yy - 16 && this.Y <= yy + 16));
        }

        internal void Respawn()
        {
            this.ModifierX = 0;
            this.ModifierY = 0;
            this.SpeedX = 0;
            this.SpeedY = 0;
            this.IsDead = false;
        }
        internal void KillPlayer()
        {
            this.deathOffset = 0;
            this.deaths++;
            this.IsDead = true;
            this.isOnFire = false;
            this.onFireDeath = 200;
            this.OnDie(new PlayerEventArgs() { Player = this, BlockX = ((int)(this.X + 8) >> 4), BlockY = ((int)(this.Y + 8) >> 4) });
        }
        internal void Reset()
        {
            this.gotCoins.Clear();
            this.gotBlueCoins.Clear();
            this.deaths = 0;
        }
        internal void RemoveCoin(int xx, int yy)
        {
            for (int i = 0; i < this.gotCoins.Count; i++)
            {
                if (this.gotCoins[i].X == xx && this.gotCoins[i].Y == yy)
                {
                    this.gotCoins.RemoveAt(i);
                    break;
                }
            }
        }
        internal void RemoveBlueCoin(int xx, int yy)
        {
            for (int i = 0; i < this.gotCoins.Count; i++)
            {
                if (this.gotCoins[i].X == xx && this.gotCoins[i].Y == yy)
                {
                    this.gotCoins.RemoveAt(i);
                    break;
                }
            }
        }
        internal void SetEffect(int effectId, bool active)
        {
            switch (effectId)
            {
                case PhysicsConfig.EffectJump:
                    this.JumpBoostEffect = active;
                    break;
                case PhysicsConfig.EffectFly:
                    this.HasLevitation = active;
                    break;
                case PhysicsConfig.EffectRun:
                    this.SpeedBoostEffect = active;
                    break;
                case PhysicsConfig.EffectProtection:
                    this.IsInvulnerable = active;
                    break;
                case PhysicsConfig.EffectCurse:
                    this.CursedEffect = active;
                    break;
                case PhysicsConfig.EffectZombie:
                    this.Zombie = active;
                    break;
            }
        }

        internal void UpdatePurpleSwitches(int id)
        {
            this.Switches[id] = !this.Switches[id];
            if (this.HostWorld.Overlaps(this))
            {
                this.Switches[id] = !this.Switches[id];
                this.tileQueue.Enqueue(id);
            }
        }
        internal void UpdateTeamDoors(int x, int y)
        {
            int _loc3_ = this.HostWorld.GetBlockData(x, y)[0];
            int _loc4_ = this.Team;
            if (this.Team != _loc3_)
            {
                this.Team = _loc3_;
                if (!this.HostWorld.Overlaps(this))
                {
                    this.tx = -1;
                    this.ty = -1;
                }
                else
                {
                    this.Team = _loc4_;
                    this.tx = x;
                    this.ty = y;
                }
            }
        }
        internal void UpdateThrust()
        {
            if (this.mory != 0)
            {
                this.SpeedY = this.SpeedY - this.currentThrust * PhysicsConfig.JumpHeight / 2 * this.mory * 0.5;
            }
            if (this.morx != 0)
            {
                this.SpeedX = this.SpeedX - this.currentThrust * PhysicsConfig.JumpHeight / 2 * this.morx * 0.5;
            }
            if (this.IsThrusting)
            {
                if (this.currentThrust > 0)
                {
                    this.currentThrust = this.currentThrust - this.thrustBurnOff;
                }
                else
                {
                    this.currentThrust = 0;
                }
            }
        }
        public void ApplyThrust()
        {
            this.currentThrust = maxThrust;
        }

        // this is used because: http://stackoverflow.com/questions/3103782/rule-of-thumb-to-test-the-equality-of-two-doubles-in-c
        internal bool DoubleIsEqual(double d1, double d2)
        {
            return Math.Abs(d1 - d2) < 0.00000001;
        }
    }

    public class PlayerEventArgs
    {
        /// <summary>
        /// Player which caused the event.
        /// </summary>
        public PhysicsPlayer Player { get; set; }

        /// <summary>
        /// Block X where event happened.
        /// </summary>
        public int BlockX { get; set; }

        /// <summary>
        /// Block Y where event happened.
        /// </summary>
        public int BlockY { get; set; }
    }
}