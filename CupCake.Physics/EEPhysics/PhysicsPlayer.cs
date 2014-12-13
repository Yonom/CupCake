using System;
using System.Collections.Generic;

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
        private int current;
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
        private bool isInvulnerable;
        private bool donex, doney;
        private int[] queue = new int[PhysicsConfig.QueueLength];
        private int delayed;
        private const double portalMultiplier = 1.42;
        private Point lastPortal;
        private List<Point> gotCoins = new List<Point>();
        private List<Point> gotBlueCoins = new List<Point>();

        /// <summary>Purple switch state.</summary>
        public bool Purple { get; internal set; }
        /// <summary>Also includes moderator and guardian mode.</summary>
        public bool InGodMode { get; internal set; }
        public bool IsDead { get; internal set; }
        //public bool Zombie { get; internal set; }
        public bool HasChat { get; internal set; }

        internal double GravityMultiplier { get { return this.HostWorld.WorldGravity; } }
        internal double SpeedMultiplier
        {
            get
            {
                /*double d = 1;
                if (Zombie) {
                    d *= 1.2;
                }
                return d;*/
                return 1;
            }
        }
        public double SpeedX { get { return this.speedX * PhysicsConfig.VariableMultiplier; } internal set { this.speedX = value / PhysicsConfig.VariableMultiplier; } }
        public double SpeedY { get { return this.speedY * PhysicsConfig.VariableMultiplier; } internal set { this.speedY = value / PhysicsConfig.VariableMultiplier; } }
        public double ModifierX { get { return this.modifierX * PhysicsConfig.VariableMultiplier; } internal set { this.modifierX = value / PhysicsConfig.VariableMultiplier; } }
        public double ModifierY { get { return this.modifierY * PhysicsConfig.VariableMultiplier; } internal set { this.modifierY = value / PhysicsConfig.VariableMultiplier; } }

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

            int cx = ((int)(this.X + 8) >> 4);
            int cy = ((int)(this.Y + 8) >> 4);

            this.current = this.HostWorld.GetBlock(0, cx, cy);
            if (this.current == 4 || ItemId.isClimbable(this.current))
            {
                this.delayed = this.queue[1];
                this.queue[0] = this.current;
            }
            else
            {
                this.delayed = this.queue[0];
                this.queue[0] = this.queue[1];
            }
            this.queue[1] = this.current;

            if (this.IsDead)
            {
                this.Horizontal = 0;
                this.Vertical = 0;
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
                switch (this.current)
                {
                    case 1:
                        this.morx = -((int)this.gravity);
                        this.mory = 0;
                        break;
                    case 2:
                        this.morx = 0;
                        this.mory = -((int)this.gravity);
                        break;
                    case 3:
                        this.morx = (int)this.gravity;
                        this.mory = 0;
                        break;
                    case ItemId.SpeedLeft:
                    case ItemId.SpeedRight:
                    case ItemId.SpeedUp:
                    case ItemId.SpeedDown:
                    case ItemId.Chain:
                    case ItemId.NinjaLadder:
                    case ItemId.WineH:
                    case ItemId.WineV:
                    case 4:
                        this.morx = 0;
                        this.mory = 0;
                        break;
                    case ItemId.Water:
                        this.morx = 0;
                        this.mory = (int)PhysicsConfig.WaterBuoyancy;
                        break;
                    case ItemId.Mud:
                        this.morx = 0;
                        this.mory = (int)PhysicsConfig.MudBuoyancy;
                        break;
                    case ItemId.Fire:
                    case ItemId.Spike:
                        if (!this.IsDead && !this.isInvulnerable)
                        {
                            this.KillPlayer();
                            this.OnDie(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                        };
                        break;
                    default:
                        this.morx = 0;
                        this.mory = (int)this.gravity;
                        break;
                }

                switch (this.delayed)
                {
                    case 1:
                        this.mox = -this.gravity;
                        this.moy = 0;
                        break;
                    case 2:
                        this.mox = 0;
                        this.moy = -this.gravity;
                        break;
                    case 3:
                        this.mox = this.gravity;
                        this.moy = 0;
                        break;
                    case ItemId.SpeedLeft:
                    case ItemId.SpeedRight:
                    case ItemId.SpeedUp:
                    case ItemId.SpeedDown:
                    case ItemId.Chain:
                    case ItemId.NinjaLadder:
                    case ItemId.WineH:
                    case ItemId.WineV:
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
                    default:
                        this.mox = 0;
                        this.moy = this.gravity;
                        break;
                }
            }

            if (this.moy == PhysicsConfig.WaterBuoyancy || this.moy == PhysicsConfig.MudBuoyancy)
            {
                this.mx = this.Horizontal;
                this.my = this.Vertical;
            }
            else
            {
                if (this.moy != 0)
                {
                    this.mx = this.Horizontal;
                    this.my = 0;
                }
                else
                {
                    if (this.mox != 0)
                    {
                        this.mx = 0;
                        this.my = this.Vertical;
                    }
                    else
                    {
                        this.mx = this.Horizontal;
                        this.my = this.Vertical;
                    }
                }
            }
            this.mx *= this.SpeedMultiplier;
            this.my *= this.SpeedMultiplier;
            this.mox *= this.GravityMultiplier;
            this.moy *= this.GravityMultiplier;

            this.ModifierX = (this.mox + this.mx);
            this.ModifierY = (this.moy + this.my);

            if (!this.DoubleIsEqual(this.speedX, 0) || this.modifierX != 0)
            {
                this.speedX = (this.speedX + this.modifierX);
                this.speedX = (this.speedX * PhysicsConfig.BaseDrag);
                if ((this.mx == 0 && this.moy != 0) || (this.speedX < 0 && this.mx > 0) || (this.speedX > 0 && this.mx < 0) || (ItemId.isClimbable(this.current) && !isGodMode))
                {
                    this.speedX = (this.speedX * PhysicsConfig.NoModifierDrag);
                }
                else
                {
                    if (this.current == ItemId.Water && !isGodMode)
                    {
                        this.speedX = (this.speedX * PhysicsConfig.WaterDrag);
                    }
                    else
                    {
                        if (this.current == ItemId.Mud && !isGodMode)
                        {
                            this.speedX = (this.speedX * PhysicsConfig.MudDrag);
                        }
                    }
                }
                if (this.speedX > 16)
                {
                    this.speedX = 16;
                }
                else
                {
                    if (this.speedX < -16)
                    {
                        this.speedX = -16;
                    }
                    else
                    {
                        if (this.speedX < 0.0001 && this.speedX > -0.0001)
                        {
                            this.speedX = 0;
                        }
                    }
                }
            }
            if (!this.DoubleIsEqual(this.speedY, 0) || this.modifierY != 0)
            {
                this.speedY = (this.speedY + this.modifierY);
                this.speedY = (this.speedY * PhysicsConfig.BaseDrag);
                if ((this.my == 0 && this.mox != 0) || (this.speedY < 0 && this.my > 0) || (this.speedY > 0 && this.my < 0) || (ItemId.isClimbable(this.current) && !isGodMode))
                {
                    this.speedY = (this.speedY * PhysicsConfig.NoModifierDrag);
                }
                else
                {
                    if (this.current == ItemId.Water && !isGodMode)
                    {
                        this.speedY = (this.speedY * PhysicsConfig.WaterDrag);
                    }
                    else
                    {
                        if (this.current == ItemId.Mud && !isGodMode)
                        {
                            this.speedY = (this.speedY * PhysicsConfig.MudDrag);
                        }
                    }
                }
                if (this.speedY > 16)
                {
                    this.speedY = 16;
                }
                else
                {
                    if (this.speedY < -16)
                    {
                        this.speedY = -16;
                    }
                    else
                    {
                        if (this.speedY < 0.0001 && this.speedY > -0.0001)
                        {
                            this.speedY = 0;
                        }
                    }
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
                    if (this.lastPortal == null)
                    {
                        this.OnHitPortal(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                        this.lastPortal = new Point(cx, cy);
                        int[] data = this.HostWorld.GetBlockData(cx, cy);
                        if (data != null && data.Length == 3)
                        {
                            Point portalPoint = this.HostWorld.GetPortalById(data[2]);
                            if (portalPoint != null)
                            {
                                int rot1 = this.HostWorld.GetBlockData(this.lastPortal.x, this.lastPortal.y)[0];
                                int rot2 = this.HostWorld.GetBlockData(portalPoint.x, portalPoint.y)[0];
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
                                this.X = portalPoint.x * 16;
                                this.Y = portalPoint.y * 16;
                                this.lastPortal = portalPoint;
                            }
                        }
                    }
                }
                else
                {
                    this.lastPortal = null;
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
                    };
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
                if (this.pastx != cx || this.pasty != cy)
                {
                    PlayerEvent e;
                    if (this.blockIdEvents.Count != 0 && this.blockIdEvents.TryGetValue(this.current, out e))
                    {
                        e(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                    }
                    if (this.bgblockIdEvents.Count != 0 && this.bgblockIdEvents.TryGetValue(this.HostWorld.GetBlock(1, cx, cy), out e)) {
                        e(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                    }

                    // Might remove specific events soon, because you can make them now with void AddBlockEvent. (except OnGetCoin and OnGetBlueCoin)
                    switch (this.current)
                    {
                        case 100:   //coin
                            this.OnHitCoin(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            for (int i = 0; i < this.gotCoins.Count; i++)
                            {
                                if (this.gotCoins[i].x == cx && this.gotCoins[i].y == cy)
                                {
                                    goto found;
                                }
                            }
                            this.OnGetCoin(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            this.gotCoins.Add(new Point(cx, cy));
                        found:
                            break;
                        case 101:   // bluecoin
                            this.OnHitBlueCoin(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            for (int i = 0; i < this.gotBlueCoins.Count; i++)
                            {
                                if (this.gotBlueCoins[i].x == cx && this.gotBlueCoins[i].y == cy)
                                {
                                    goto found2;
                                }
                            }
                            this.OnGetBlueCoin(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            this.gotBlueCoins.Add(new Point(cx, cy));
                        found2:
                            break;
                        case 5:
                            // crown
                            this.OnHitCrown(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            break;
                        case 6:
                            // red key
                            this.OnHitRedKey(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            break;
                        case 7:
                            // green key
                            this.OnHitGreenKey(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            break;
                        case 8:
                            // blue key
                            this.OnHitBlueKey(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            break;
                        case ItemId.SwitchPurple:
                            this.Purple = !this.Purple;
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
                            break;
                        case ItemId.Cake:
                            this.OnHitCake(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            break;
                        case ItemId.Checkpoint:
                            if (!isGodMode)
                            {
                                this.LastCheckpointX = cx;
                                this.LastCheckpointY = cy;
                                this.OnHitCheckpoint(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            }
                            break;
                        case ItemId.BrickComplete:
                            this.OnHitCompleteLevelBrick(new PlayerEventArgs() { Player = this, BlockX = cx, BlockY = cy });
                            break;
                    }
                    this.pastx = cx;
                    this.pasty = cy;
                }
            }

            var imx = ((int)this.speedX << 8);
            var imy = ((int)this.speedY << 8);

            if (this.current != ItemId.Water && this.current != ItemId.Mud)
            {
                if (imx == 0)
                {
                    if (this.modifierX < 0.1 && this.modifierX > -0.1)
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
                            };
                        }
                        else
                        {
                            if (tx > 14)
                            {
                                if (tx > 15.8)
                                {
                                    this.X = Math.Ceiling(this.X);
                                }
                                else
                                {
                                    this.X += (tx - 14) / 15;
                                }
                            }
                        }
                    }
                }

                if (imy == 0)
                {
                    if ((this.modifierY < 0.1) && (this.modifierY > -0.1))
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
                                    this.Y = Math.Ceiling(this.Y);
                                }
                                else
                                {
                                    this.Y += (ty - 14) / 15;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Makes PhysicsPlayer raise event when player overlaps blockId block. Event is not raised every tick, but only at when player first touches a block.
        /// (Event isn't raised if player only touches the block, the player needs to be inside it)
        /// </summary>
        /// <param name="blockId">Block ID to upon overlaps raise the event.</param>
        /// <param name="e">Method which is run when event occurs.</param>
        public void AddBlockEvent(int blockId, PlayerEvent e)
        {
            if (blockId < 500)
                this.blockIdEvents[blockId] = e;
            else
                this.bgblockIdEvents[blockId] = e;
        }
        /// <returns>Whether there's block event with specified blockId.</returns>
        public bool HasBlockEvent(int blockId)
        {
            if (blockId < 500)
                return this.blockIdEvents.ContainsKey(blockId);
            else
                return this.bgblockIdEvents.ContainsKey(blockId);
        }
        /// <summary>
        /// Removes block event added with AddBlockEvent with specified blockId.
        /// </summary>
        public void RemoveBlockEvent(int blockId)
        {
            if (blockId < 500)
                this.blockIdEvents.Remove(blockId);
            else
                this.bgblockIdEvents.Remove(blockId);
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
            this.IsDead = true;
        }
        internal void ResetCoins()
        {
            this.gotCoins.Clear();
            this.gotBlueCoins.Clear();
        }
        internal void RemoveCoin(int xx, int yy)
        {
            for (int i = 0; i < this.gotCoins.Count; i++)
            {
                if (this.gotCoins[i].x == xx && this.gotCoins[i].y == yy)
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
                if (this.gotCoins[i].x == xx && this.gotCoins[i].y == yy)
                {
                    this.gotCoins.RemoveAt(i);
                    break;
                }
            }
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

    internal class Point
    {
        public int x, y;
        public Point(int xx, int yy)
        {
            this.x = xx;
            this.y = yy;
        }
    }
}
