using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using PlayerIOClient;

namespace CupCake.Physics.EEPhysics
{
    public class PhysicsWorld
    {
        internal const int Size = 16;
        internal Stopwatch sw = new Stopwatch();
        private List<Message> earlyMessages = new List<Message>();
        private bool inited;
        private bool running;
        private Thread physicsThread;

        private int[][][] blocks;
        private int[][][] blockData;
        private bool hideRed, hideBlue, hideGreen, hideTimedoor;
        internal double WorldGravity = 1;

        public int WorldWidth { get; private set; }
        public int WorldHeight { get; private set; }

        /// <summary>
        /// Whether bot automatically starts the physics simulation when it gets init message. Defaults to true.
        /// </summary>
        public bool AutoStart { get; set; }
        /// <summary>
        /// Whether bot adds itself from init message. Defaults to true.
        /// </summary>
        public bool AddBotPlayer { get; set; }
        /// <summary>
        /// Whether physics simulation thread has been started.
        /// </summary>
        public bool PhysicsRunning { get; private set; }

        /// <summary>
        /// You shouldn't add or remove any items from this dictionary outside EEPhysics.
        /// </summary>
        public ConcurrentDictionary<int, PhysicsPlayer> Players { get; private set; }
        public string WorldKey { get; private set; }
        /// <summary>
        /// Called upon every physics simulation tick. (every 10ms)
        /// </summary>
        public event EventHandler OnTick = delegate { };

        public PhysicsWorld()
        {
            this.AutoStart = true;
            this.AddBotPlayer = true;
            this.Players = new ConcurrentDictionary<int, PhysicsPlayer>();
        }

        /// <summary>
        /// Will run the physics simulation. Needs to be called only once. If you have AutoStart set to true or you started physics with StartSimulation, don't call this!
        /// </summary>
        public void Run()
        {
            this.running = true;
            this.PhysicsRunning = true;

            this.sw.Start();
            while (this.running)
            {
                long frameStartTime = this.sw.ElapsedMilliseconds;
                foreach (KeyValuePair<int, PhysicsPlayer> pair in this.Players)
                {
                    pair.Value.Tick();
                }
                this.OnTick(this, null);
                long frameEndTime = this.sw.ElapsedMilliseconds;
                long waitTime = 10 - (frameEndTime - frameStartTime);
                if (waitTime > 0)
                    Thread.Sleep((int)waitTime);
            }

            this.PhysicsRunning = false;
        }

        /// <summary>
        /// Call this for every PlayerIO Message you receive.
        /// </summary>
        public void HandleMessage(Message m)
        {
            if (!this.inited)
            {
                if (m.Type == "init")
                {
                    this.WorldWidth = m.GetInt(12);
                    this.WorldHeight = m.GetInt(13);

                    this.blocks = new int[2][][];
                    for (int i = 0; i < this.blocks.Length; i++)
                    {
                        this.blocks[i] = new int[this.WorldWidth][];
                        for (int ii = 0; ii < this.WorldWidth; ii++)
                            this.blocks[i][ii] = new int[this.WorldHeight];
                    }

                    this.blockData = new int[this.WorldWidth][][];
                    for (int i = 0; i < this.WorldWidth; i++)
                        this.blockData[i] = new int[this.WorldHeight][];

                    this.WorldKey = Derot(m.GetString(5));
                    this.WorldGravity = m.GetDouble(15);

                    if (this.AddBotPlayer)
                    {
                        PhysicsPlayer p = new PhysicsPlayer(m.GetInt(6), m.GetString(9))
                        {
                            X = m.GetInt(7),
                            Y = m.GetInt(8),
                            HostWorld = this
                        };
                        this.Players.TryAdd(p.ID, p);
                    }

                    this.DeserializeBlocks(m, (m[19] is string) ? 20u : 19u);
                    this.inited = true;

                    foreach (Message m2 in this.earlyMessages)
                    {
                        this.HandleMessage(m2);
                    }
                    this.earlyMessages.Clear();

                    if (this.AutoStart && (this.physicsThread == null || !this.physicsThread.IsAlive))
                    {
                        this.StartSimulation();
                    }
                }
                else if (m.Type != "add" && m.Type != "left")
                {
                    this.earlyMessages.Add(m);
                    return;
                }
            }
            switch (m.Type)
            {
                case "m":
                    {
                        PhysicsPlayer p;
                        if (this.Players.TryGetValue(m.GetInt(0), out p))
                        {
                            p.X = m.GetDouble(1);
                            p.Y = m.GetDouble(2);
                            p.SpeedX = m.GetDouble(3);
                            p.SpeedY = m.GetDouble(4);
                            p.ModifierX = m.GetDouble(5);
                            p.ModifierY = m.GetDouble(6);
                            p.Horizontal = m.GetInt(7);
                            p.Vertical = m.GetInt(8);
                            p.Coins = m.GetInt(9);
                            p.Purple = m.GetBoolean(10);
                            p.IsDead = false;
                        }
                    }
                    break;
                case "b":
                    {
                        int zz = m.GetInt(0);
                        int xx = m.GetInt(1);
                        int yy = m.GetInt(2);
                        int blockId = m.GetInt(3);
                        if (zz == 0)
                        {
                            switch (this.blocks[zz][xx][yy])
                            {
                                case 100:
                                    foreach (KeyValuePair<int, PhysicsPlayer> pair in this.Players)
                                    {
                                        pair.Value.RemoveCoin(xx, yy);
                                    }
                                    break;
                                case 101:
                                    foreach (KeyValuePair<int, PhysicsPlayer> pair in this.Players)
                                    {
                                        pair.Value.RemoveBlueCoin(xx, yy);
                                    }
                                    break;
                            }
                        }
                        this.blocks[zz][xx][yy] = blockId;
                    }
                    break;
                case "add":
                    {
                        PhysicsPlayer p = new PhysicsPlayer(m.GetInt(0), m.GetString(1));
                        p.HostWorld = this;
                        p.X = m.GetDouble(3);
                        p.Y = m.GetDouble(4);
                        p.InGodMode = m.GetBoolean(5);
                        p.HasChat = m.GetBoolean(7);
                        p.Coins = m.GetInt(8);
                        p.BlueCoins = m.GetInt(9);
                        p.Purple = m.GetBoolean(11);
                        p.IsClubMember = m.GetBoolean(13);

                        this.Players.TryAdd(p.ID, p);
                    }
                    break;
                case "left":
                    {
                        PhysicsPlayer p;
                        this.Players.TryRemove(m.GetInt(0), out p);
                    }
                    break;
                case "show":
                case "hide":
                    {
                        bool b = (m.Type == "hide");
                        switch (m.GetString(0))
                        {
                            case "timedoor":
                                this.hideTimedoor = b;
                                break;
                            case "blue":
                                this.hideBlue = b;
                                break;
                            case "red":
                                this.hideRed = b;
                                break;
                            case "green":
                                this.hideGreen = b;
                                break;
                        }
                    }
                    break;
                case "c":
                    {
                        PhysicsPlayer p;
                        if (this.Players.TryGetValue(m.GetInt(0), out p))
                        {
                            p.Coins = m.GetInt(1);
                            p.BlueCoins = m.GetInt(2);
                        }
                    }
                    break;
                case "bc":
                case "br":
                case "bs":
                    {
                        int xx = m.GetInt(0);
                        int yy = m.GetInt(1);
                        this.blocks[0][xx][yy] = m.GetInt(2);
                        this.blockData[xx][yy] = new int[m.Count - 4];
                        for (uint i = 3; i < 4; i++)
                        {
                            this.blockData[xx][yy][i - 3] = m.GetInt(i);
                        }
                    }
                    break;
                case "pt":
                    {
                        int xx = m.GetInt(0);
                        int yy = m.GetInt(1);
                        this.blocks[0][xx][yy] = m.GetInt(2);
                        this.blockData[xx][yy] = new int[m.Count - 3];
                        for (uint i = 3; i < 6; i++)
                        {
                            this.blockData[xx][yy][i - 3] = m.GetInt(i);
                        }
                    }
                    break;
                case "god":
                case "guardian":
                case "mod":
                    {
                        PhysicsPlayer p;
                        if (this.Players.TryGetValue(m.GetInt(0), out p))
                        {
                            p.InGodMode = m.GetBoolean(1);
                            p.Respawn();
                        }
                    }
                    break;
                case "tele":
                    {
                        bool b = m.GetBoolean(0);
                        uint i = 1;
                        while (i + 2 < m.Count)
                        {
                            PhysicsPlayer p;
                            if (this.Players.TryGetValue(m.GetInt(i), out p))
                            {
                                p.X = m.GetInt(i + 1);
                                p.Y = m.GetInt(i + 2);
                                if (b)
                                {
                                    p.Respawn();
                                }
                            }
                            i += 3;
                        }
                    }
                    break;
                case "teleport":
                    {
                        PhysicsPlayer p;
                        if (this.Players.TryGetValue(m.GetInt(0), out p))
                        {
                            p.X = m.GetInt(1);
                            p.Y = m.GetInt(2);
                        }
                    }
                    break;
                case "reset":
                    {
                        this.DeserializeBlocks(m, 1);
                        foreach (KeyValuePair<int, PhysicsPlayer> pair in this.Players)
                        {
                            pair.Value.ResetCoins();
                        }
                        /*for (int i = 0; i < players.Count; i++) {
                            players[i].Coins = 0;
                            players[i].respawn();
                        }*/
                    }
                    break;
                case "clear":
                    {
                        int border = m.GetInt(2);
                        int fill = m.GetInt(3);
                        for (int i = 0; i < this.WorldWidth; i++)
                        {
                            for (int ii = 0; ii < this.WorldHeight; ii++)
                            {
                                if (i == 0 || ii == 0 || i == this.WorldWidth - 1 || ii == this.WorldHeight - 1)
                                {
                                    this.blocks[0][i][ii] = border;
                                }
                                else
                                {
                                    this.blocks[0][i][ii] = fill;
                                }
                                this.blocks[1][i][ii] = 0;
                            }
                        }
                        foreach (KeyValuePair<int, PhysicsPlayer> pair in this.Players)
                            pair.Value.ResetCoins();
                    }
                    break;
                case "ts":
                case "lb":
                case "wp":
                    {
                        this.blocks[0][m.GetInt(0)][m.GetInt(1)] = m.GetInt(2);
                    }
                    break;
            }
        }

        /// <returns>Foreground block ID</returns>
        public int GetBlock(int x, int y)
        {
            return this.GetBlock(0, x, y);
        }
        /// <param name="z">Block layer: 0 = foreground, 1 = background</param>
        /// <param name="x">Block X</param>
        /// <param name="z">Block Y</param>
        /// <returns>Block ID</returns>
        public int GetBlock(int z, int x, int y)
        {
            if (z < 0 || z > 1)
            {
                throw new ArgumentOutOfRangeException("zz", "Layer must be 0 (foreground) or 1 (background).");
            }
            if (x < 0 || x >= this.WorldWidth || y < 0 || y >= this.WorldHeight)
            {
                return -1;
            }
            return this.blocks[z][x][y];
        }
        /// <returns>Extra block data, eg. rotation, id and target id from portals. Doesn't support signs.</returns>
        public int[] GetBlockData(int x, int y)
        {
            if (x < 0 || x >= this.WorldWidth || y < 0 || y >= this.WorldHeight)
            {
                return null;
            }
            return this.blockData[x][y];
        }
        internal Point GetPortalById(int id)
        {
            for (int i = 0; i < this.WorldWidth; i++)
            {
                for (int ii = 0; ii < this.WorldHeight; ii++)
                {
                    if (this.blocks[0][i][ii] == 242 || this.blocks[0][i][ii] == 381)
                    {
                        if (this.blockData[i][ii][1] == id)
                        {
                            return new Point(i, ii);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Starts the physics simulation in another thread.
        /// </summary>
        public void StartSimulation()
        {
            if (!this.PhysicsRunning)
            {
                if (this.inited)
                {
                    this.physicsThread = new Thread(this.Run) {IsBackground = true};
                    this.physicsThread.Start();
                }
                else
                {
                    throw new Exception("Cannot start before bot has received init message.");
                }
            }
            else
            {
                throw new Exception("Simulation thread has already been started.");
            }
        }

        /// <summary>
        /// Stops the physics simulation thread.
        /// </summary>
        public void StopSimulation()
        {
            if (this.PhysicsRunning)
            {
                this.running = false;
            }
            else
            {
                throw new Exception("Simulation thread is not running.");
            }
        }

        internal bool Overlaps(PhysicsPlayer p)
        {
            if ((p.X < 0 || p.Y < 0) || ((p.X > this.WorldWidth * 16 - 16) || (p.Y > this.WorldHeight * 16 - 16)))
            {
                return true;
            }
            if (p.InGodMode)
            {
                return false;
            }
            int tileId;
            var firstX = ((int)p.X >> 4);
            var firstY = ((int)p.Y >> 4);
            double lastX = ((p.X + PhysicsPlayer.Height) / Size);
            double lastY = ((p.Y + PhysicsPlayer.Width) / Size);
            bool _local7 = false;

            int x;
            int y = firstY;
            while (y < lastY)
            {
                x = firstX;
                for (; x < lastX; x++)
                {
                    tileId = this.blocks[0][x][y];
                    if (ItemId.isSolid(tileId))
                    {
                        switch (tileId)
                        {
                            case 23:
                                if (this.hideRed)
                                {
                                    continue;
                                }
                                break;
                            case 24:
                                if (this.hideGreen)
                                {
                                    continue;
                                }
                                break;
                            case 25:
                                if (this.hideBlue)
                                {
                                    continue;
                                }
                                break;
                            case 26:
                                if (!this.hideRed)
                                {
                                    continue;
                                }
                                break;
                            case 27:
                                if (!this.hideGreen)
                                {
                                    continue;
                                }
                                break;
                            case 28:
                                if (!this.hideBlue)
                                {
                                    continue;
                                }
                                break;
                            case 156:
                                if (this.hideTimedoor)
                                {
                                    continue;
                                }
                                break;
                            case 157:
                                if (!this.hideTimedoor)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.DoorPurple:
                                if (p.Purple)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.GatePurple:
                                if (!p.Purple)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.DoorClub:
                                if (p.IsClubMember)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.GateClub:
                                if (!p.IsClubMember)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.Coindoor:
                            case ItemId.BlueCoindoor:
                                if (this.blockData[x][y][0] <= p.Coins)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.Coingate:
                            case ItemId.BlueCoingate:
                                if (this.blockData[x][y][0] > p.Coins)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.ZombieGate:
                                /*if (p.Zombie) {
                                    continue;
                                };*/
                                break;
                            case ItemId.ZombieDoor:
                                /*if (!p.Zombie) {
                                    continue;
                                };*/
                                continue;
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
                                if (p.SpeedY < 0 || y <= p.overlapy)
                                {
                                    if (y != firstY || p.overlapy == -1)
                                    {
                                        p.overlapy = y;
                                    }
                                    _local7 = true;
                                    continue;
                                }
                                break;
                            case 83:
                            case 77:
                                continue;
                        };
                        return true;
                    }
                }
                y++;
            }
            if (!_local7)
            {
                p.overlapy = -1;
            }
            return false;
        }


        internal static string Derot(string arg1)
        {
            // by Capasha (http://pastebin.com/Pj6tvNNx)
            int num = 0;
            string str = "";
            for (int i = 0; i < arg1.Length; i++)
            {
                num = arg1[i];
                if ((num >= 0x61) && (num <= 0x7a))
                {
                    if (num > 0x6d) num -= 13;
                    else num += 13;
                }
                else if ((num >= 0x41) && (num <= 90))
                {
                    if (num > 0x4d) num -= 13;
                    else num += 13;
                }
                str = str + ((char)num);
            }
            return str;
        }

        internal void DeserializeBlocks(Message m, uint start)
        {
            // Got and modified from Skylight by TakoMan02 (made originally in VB by Bass5098), credit to them
            // (http://seist.github.io/Skylight/)
            // > https://github.com/Seist/Skylight/blob/master/Skylight/Miscellaneous/Tools.cs, method ConvertMessageToBlockList
            try
            {
                uint messageIndex = start;
                while (messageIndex < m.Count)
                {
                    if (m[messageIndex] is string)
                    {
                        break;
                    }

                    int blockId = m.GetInteger(messageIndex);
                    messageIndex++;

                    int z = m.GetInteger(messageIndex);
                    messageIndex++;

                    byte[] xa = m.GetByteArray(messageIndex);
                    messageIndex++;

                    byte[] ya = m.GetByteArray(messageIndex);
                    messageIndex++;

                    List<int> data = new List<int>();
                    switch (blockId) {
                        case ItemId.WorldPortal:
                        case ItemId.TextSign:
                            messageIndex++;
                            break;
                        case ItemId.Coindoor:
                        case ItemId.Coingate:
                        case ItemId.BlueCoindoor:
                        case ItemId.BlueCoingate:
                        case ItemId.Spike:
                        case ItemId.Piano:
                        case ItemId.Drum:
                        case ItemId.GlowylineBlueSlope:
                        case ItemId.GlowyLineBlueStraight:
                        case ItemId.GlowyLineYellowSlope:
                        case ItemId.GlowyLineYellowStraight:
                        case ItemId.GlowyLineGreenSlope:
                        case ItemId.GlowyLineGreenStraight:
                            data.Add(m.GetInteger(messageIndex));
                            messageIndex++;
                            break;
                        case ItemId.Portal:
                        case ItemId.PortalInvisible:
                            for (int i = 0; i < 3; i++) {
                                data.Add(m.GetInteger(messageIndex));
                                messageIndex++;
                            }
                            break;
                        default:
                            break;
                    }
                    int x = 0, y = 0;

                    for (int pos = 0; pos < ya.Length; pos += 2)
                    {
                        x = (xa[pos] * 256) + xa[pos + 1];
                        y = (ya[pos] * 256) + ya[pos + 1];

                        this.blocks[z][x][y] = blockId;
                        if (data.Count > 0)
                        {
                            this.blockData[x][y] = data.ToArray();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(" EEPhysics: Error loading existing blocks:\n" + e);
            }
        }
    }
}
