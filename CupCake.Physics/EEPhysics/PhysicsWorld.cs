using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using PlayerIOClient;

namespace CupCake.Physics.EEPhysics
{
    public class PhysicsWorld : IDisposable 
    {
        internal const int Size = 16;
        internal Stopwatch sw = new Stopwatch();
        private List<Message> earlyMessages = new List<Message>();
        private bool inited;
        private bool running;
        private Thread physicsThread;

        private int[][][] blocks;
        private int[][][] blockData;
        private bool hideRed, hideBlue, hideGreen, hideCyan, hideMagenta, hideYellow, hideTimedoor;
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
            AutoStart = true;
            AddBotPlayer = true;
            Players = new ConcurrentDictionary<int, PhysicsPlayer>();
        }

        /// <summary>
        /// Will run the physics simulation. Needs to be called only once. If you have AutoStart set to true or you started physics with StartSimulation, don't call this!
        /// </summary>
        public void Run()
        {
            running = true;
            PhysicsRunning = true;

            sw.Start();
            while (running)
            {
                long frameStartTime = sw.ElapsedMilliseconds;
                foreach (KeyValuePair<int, PhysicsPlayer> pair in Players)
                {
                    pair.Value.Tick();
                }
                OnTick(this, null);
                long frameEndTime = sw.ElapsedMilliseconds;
                long waitTime = 10 - (frameEndTime - frameStartTime);
                if (waitTime > 0)
                    Thread.Sleep((int)waitTime);
            }

            PhysicsRunning = false;
        }

        /// <summary>
        /// Call this for every PlayerIO Message you receive.
        /// </summary>
        public void HandleMessage(Message m)
        {
            if (!inited)
            {
                if (m.Type == "init")
                {
                    WorldWidth = m.GetInt(12);
                    WorldHeight = m.GetInt(13);

                    blocks = new int[2][][];
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        blocks[i] = new int[WorldWidth][];
                        for (int ii = 0; ii < WorldWidth; ii++)
                            blocks[i][ii] = new int[WorldHeight];
                    }

                    blockData = new int[WorldWidth][][];
                    for (int i = 0; i < WorldWidth; i++)
                        blockData[i] = new int[WorldHeight][];

                    WorldKey = Derot(m.GetString(5));
                    WorldGravity = m.GetDouble(15);

                    if (AddBotPlayer)
                    {
                        PhysicsPlayer p = new PhysicsPlayer(m.GetInt(6), m.GetString(9))
                        {
                            X = m.GetInt(7),
                            Y = m.GetInt(8),
                            HostWorld = this
                        };
                        Players.TryAdd(p.ID, p);
                    }

                    DeserializeBlocks(m, (m[19] is string) ? 20u : 19u);
                    inited = true;

                    foreach (Message m2 in earlyMessages)
                    {
                        HandleMessage(m2);
                    }
                    earlyMessages.Clear();

                    if (AutoStart && (physicsThread == null || !physicsThread.IsAlive))
                    {
                        StartSimulation();
                    }
                }
                else if (m.Type != "add" && m.Type != "left")
                {
                    earlyMessages.Add(m);
                    return;
                }
            }
            switch (m.Type)
            {
                case "m":
                    {
                        PhysicsPlayer p;
                        if (Players.TryGetValue(m.GetInt(0), out p))
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
                            switch (blocks[zz][xx][yy])
                            {
                                case 100:
                                    foreach (KeyValuePair<int, PhysicsPlayer> pair in Players)
                                    {
                                        pair.Value.RemoveCoin(xx, yy);
                                    }
                                    break;
                                case 101:
                                    foreach (KeyValuePair<int, PhysicsPlayer> pair in Players)
                                    {
                                        pair.Value.RemoveBlueCoin(xx, yy);
                                    }
                                    break;
                            }
                        }
                        blocks[zz][xx][yy] = blockId;
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
                        p.IsClubMember = m.GetBoolean(13);

                        Players.TryAdd(p.ID, p);
                    }
                    break;
                case "left":
                    {
                        PhysicsPlayer p;
                        Players.TryRemove(m.GetInt(0), out p);
                    }
                    break;
                case "show":
                case "hide":
                    {
                        bool b = (m.Type == "hide");
                        switch (m.GetString(0))
                        {
                            case "timedoor":
                                hideTimedoor = b;
                                break;
                            case "blue":
                                hideBlue = b;
                                break;
                            case "red":
                                hideRed = b;
                                break;
                            case "green":
                                hideGreen = b;
                                break;
                            case "cyan":
                                hideCyan = b;
                                break;
                            case "magenta":
                                hideMagenta = b;
                                break;
                            case "yellow":
                                hideYellow = b;
                                break;
                        }
                    }
                    break;
                case "c":
                    {
                        PhysicsPlayer p;
                        if (Players.TryGetValue(m.GetInt(0), out p))
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
                        blocks[0][xx][yy] = m.GetInt(2);
                        blockData[xx][yy] = new int[1];
                        for (uint i = 3; i < 4; i++)
                        {
                            blockData[xx][yy][i - 3] = m.GetInt(i);
                        }
                    }
                    break;
                case "pt":
                    {
                        int xx = m.GetInt(0);
                        int yy = m.GetInt(1);
                        blocks[0][xx][yy] = m.GetInt(2);
                        blockData[xx][yy] = new int[3];
                        for (uint i = 3; i < 6; i++)
                        {
                            blockData[xx][yy][i - 3] = m.GetInt(i);
                        }
                    }
                    break;
                case "god":
                case "guardian":
                case "mod":
                    {
                        PhysicsPlayer p;
                        if (Players.TryGetValue(m.GetInt(0), out p))
                        {
                            p.InGodMode = m.GetBoolean(1);
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
                            if (Players.TryGetValue(m.GetInt(i), out p))
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
                        if (Players.TryGetValue(m.GetInt(0), out p))
                        {
                            p.X = m.GetInt(1);
                            p.Y = m.GetInt(2);
                        }
                    }
                    break;
                case "reset":
                    {
                        DeserializeBlocks(m, 1);
                        foreach (KeyValuePair<int, PhysicsPlayer> pair in Players)
                        {
                            pair.Value.Reset();
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
                        for (int i = 0; i < WorldWidth; i++)
                        {
                            for (int ii = 0; ii < WorldHeight; ii++)
                            {
                                if (i == 0 || ii == 0 || i == WorldWidth - 1 || ii == WorldHeight - 1)
                                {
                                    blocks[0][i][ii] = border;
                                }
                                else
                                {
                                    blocks[0][i][ii] = fill;
                                }
                                blocks[1][i][ii] = 0;
                            }
                        }
                        foreach (KeyValuePair<int, PhysicsPlayer> pair in Players)
                            pair.Value.Reset();
                    }
                    break;
                case "ts":
                case "lb":
                case "wp":
                    {
                        blocks[0][m.GetInt(0)][m.GetInt(1)] = m.GetInt(2);
                    }
                    break;
                case "kill":
                    {
                        int userId = m.GetInt(0u);

                        PhysicsPlayer p;
                        if (Players.TryGetValue(userId, out p))
                        {
                            p.deaths++;
                        }
                    }
                    break;
            }
        }

        /// <returns>Foreground block ID</returns>
        public int GetBlock(int x, int y)
        {
            return GetBlock(0, x, y);
        }
        /// <param name="z">Block layer: 0 = foreground, 1 = background</param>
        /// <param name="x">Block X</param>
        /// <param name="y">Block Y</param>
        /// <returns>Block ID</returns>
        public int GetBlock(int z, int x, int y)
        {
            if (z < 0 || z > 1)
            {
                throw new ArgumentOutOfRangeException("z", "Layer must be 0 (foreground) or 1 (background).");
            }
            if (x < 0 || x >= WorldWidth || y < 0 || y >= WorldHeight)
            {
                return -1;
            }
            return blocks[z][x][y];
        }
        /// <returns>Extra block data, eg. rotation, id and target id from portals. Doesn't support signs.</returns>
        public int[] GetBlockData(int x, int y)
        {
            if (x < 0 || x >= WorldWidth || y < 0 || y >= WorldHeight)
            {
                return null;
            }
            return blockData[x][y];
        }
        internal Point GetPortalById(int id)
        {
            for (int i = 0; i < WorldWidth; i++)
            {
                for (int ii = 0; ii < WorldHeight; ii++)
                {
                    if (blocks[0][i][ii] == 242 || blocks[0][i][ii] == 381)
                    {
                        if (blockData[i][ii][1] == id)
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
            if (!PhysicsRunning)
            {
                if (inited)
                {
                    physicsThread = new Thread(Run) {IsBackground = true};
                    physicsThread.Start();
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
            if (PhysicsRunning)
            {
                running = false;
            }
            else
            {
                throw new Exception("Simulation thread is not running.");
            }
        }

        internal bool Overlaps(PhysicsPlayer p)
        {
            if ((p.X < 0 || p.Y < 0) || ((p.X > WorldWidth * 16 - 16) || (p.Y > WorldHeight * 16 - 16)))
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
            bool skip = false;

            int x;
            int y = firstY;

            int a = firstY;
            int b;
            while (y < lastY)
            {
                x = firstX;
                b = firstX;
                for (; x < lastX; x++)
                {
                    tileId = blocks[0][x][y];

                    if (ItemId.isSolid(tileId))
                    {
                        if (ItemId.canJumpThroughFromBelow(tileId))
                        {
                            int rot = blockData[x][y][0];
                            if (tileId == ItemId.Oneway_Cyan || tileId == ItemId.Oneway_Pink || tileId == ItemId.Oneway_Red || tileId == ItemId.Oneway_Yellow)
                            {
                                if ((p.SpeedY < 0 || a <= p.overlapy) && rot == 1)
                                {
                                    if (a != firstY || p.overlapy == -1)
                                    {
                                        p.overlapy = a;
                                    }

                                    skip = true;
                                    continue;
                                }

                                if ((p.SpeedX > 0 || b <= p.overlapy) && rot == 2)
                                {
                                    if (b == firstX || p.overlapy == -1)
                                    {
                                        p.overlapy = b;
                                    }

                                    skip = true;
                                    continue;
                                }

                                if ((p.SpeedY > 0 || a <= p.overlapy) && rot == 3)
                                {
                                    if (a == firstY || p.overlapy == -1)
                                    {
                                        p.overlapy = a;
                                    }

                                    skip = true;
                                    continue;
                                }
                                if ((p.SpeedX < 0 || b <= p.overlapy) && rot == 0)
                                {
                                    if (b != firstX || p.overlapy == -1)
                                    {
                                        p.overlapy = b;
                                    }

                                    skip = true;
                                    continue;
                                }
                            }
                            else
                            {
                                if (p.SpeedY < 0 || a <= p.overlapy)
                                {
                                    if (a != y || p.overlapy == -1)
                                    {
                                        p.overlapy = a;
                                    }

                                    skip = true;
                                    continue;
                                }
                            }
                        }

                        switch (tileId)
                        {
                            case 23:
                                if (hideRed)
                                {
                                    continue;
                                }
                                break;
                            case 24:
                                if (hideGreen)
                                {
                                    continue;
                                }
                                break;
                            case 25:
                                if (hideBlue)
                                {
                                    continue;
                                }
                                break;
                            case 26:
                                if (!hideRed)
                                {
                                    continue;
                                }
                                break;
                            case 27:
                                if (!hideGreen)
                                {
                                    continue;
                                }
                                break;
                            case 28:
                                if (!hideBlue)
                                {
                                    continue;
                                }
                                break;
                            case 156:
                                if (hideTimedoor)
                                {
                                    continue;
                                }
                                break;
                            case 157:
                                if (!hideTimedoor)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.CyanDoor:
                                if (hideCyan)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.MagentaDoor:
                                if (hideMagenta)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.YellowDoor:
                                if (hideYellow)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.CyanGate:
                                if (!hideCyan)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.MagentaGate:
                                if (!hideMagenta)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.YellowGate:
                                if (!hideYellow)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.DoorPurple:
                                {
                                    int pid = blockData[x][y][0];
                                    if (p.switches[pid])
                                    {
                                        continue;
                                    }
                                }
                                break;
                            case ItemId.GatePurple:
                                {
                                    int pid = blockData[x][y][0];
                                    if (!p.switches[pid])
                                    {
                                        continue;
                                    }
                                }
                                break;
                            case ItemId.Death_Door:
                                if (p.deaths < blockData[x][y][0])
                                {
                                    continue;
                                }
                                break;
                            case ItemId.Death_Gate:
                                if (p.deaths >= blockData[x][y][0])
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
                                if (blockData[x][y][0] <= p.Coins)
                                {
                                    continue;
                                }
                                break;
                            case ItemId.Coingate:
                            case ItemId.BlueCoingate:
                                if (blockData[x][y][0] > p.Coins)
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
                                    skip = true;
                                    continue;
                                }
                                break;
                            case 83:
                            case 77:
                                continue;
                        }
                        return true;
                    }
                }
                y++;
            }
            if (!skip)
            {
                p.overlapy = -1;
            }
            return false;
        }

        internal static string Derot(string arg1)
        {
            // by Capasha (http://pastebin.com/Pj6tvNNx)
            int num;
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
                        case ItemId.Death_Door:
                        case ItemId.Death_Gate:
                        case ItemId.BlueCoindoor:
                        case ItemId.BlueCoingate:
                        case ItemId.DoorPurple:
                        case ItemId.GatePurple:
                        case ItemId.SwitchPurple:
                        case ItemId.Spike:
                        case ItemId.Piano:
                        case ItemId.Drum:
                        case ItemId.Oneway_Cyan:
                        case ItemId.Oneway_Pink:
                        case ItemId.Oneway_Red:
                        case ItemId.Oneway_Yellow:
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
                        case 1000:
                            messageIndex += 2;
                            break;
                    }
                    int x, y;

                    for (int pos = 0; pos < ya.Length; pos += 2)
                    {
                        x = (xa[pos] * 256) + xa[pos + 1];
                        y = (ya[pos] * 256) + ya[pos + 1];

                        blocks[z][x][y] = blockId;
                        if (data.Count > 0)
                        {
                            blockData[x][y] = data.ToArray();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("EEPhysics: Error loading existing blocks:\n" + e);
            }
        }

        void IDisposable.Dispose()
        {
            StopSimulation();
        }
    }
}
