using System;
using System.Diagnostics;
using System.Reflection;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Core.Metadata;
using CupCake.Messages;
using CupCake.Messages.Receive;
using CupCake.Messages.User;
using CupCake.Players.Join;

namespace CupCake.Players
{
    [DebuggerDisplay("Username = {Username}, Smiley = {Smiley}")]
    public sealed class Player : CupCakeServicePart<JoinArgs>
    {
        public MetadataStore Metadata { get; private set; }

        public string Username { get; private set; }
        public int UserId { get; private set; }

        public bool IsGod { get; private set; }
        public bool IsMod { get; private set; }
        public bool IsDead { get; private set; }
        public bool IsMyFriend { get; private set; }
        public bool IsClubMember { get; private set; }
        public bool IsDisconnected { get; private set; }

        public bool HasChat { get; private set; }
        public MagicClass MagicClass { get; private set; }
        public Smiley Smiley { get; private set; }

        public int Coins { get; private set; }
        public bool SwitchOpened { get; private set; }

        public int SpawnX { get; private set; }
        public int SpawnY { get; private set; }
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        public double SpeedX { get; private set; }
        public double SpeedY { get; private set; }
        public double ModifierX { get; private set; }
        public double ModifierY { get; private set; }
        public double Vertical { get; private set; }

        public double Horizontal { get; private set; }
        public string Say { get; private set; }

        public string AutoText { get; private set; }

        public bool HasSilverCrown { get; private set; }
        public bool HasWooted { get; private set; }

        public bool RedAuraPotion { get; private set; }
        public bool BlueAuraPotion { get; private set; }
        public bool YellowAuraPotion { get; private set; }
        public bool GreenAuraPotion { get; private set; }
        public bool JumpPotion { get; private set; }
        public bool FirePotion { get; private set; }
        public bool CursePotion { get; private set; }
        public bool ProtectionPotion { get; private set; }
        public bool ZombiePotion { get; private set; }
        public bool RespawnPotion { get; private set; }
        public bool LevitationPotion { get; private set; }
        public bool FlauntPotion { get; private set; }

        public bool SolitudePotion { get; private set; }
        public Potion LastPotion { get; private set; }
        public bool LastPotionEnabled { get; private set; }
        public int LastPotionTimeout { get; private set; }

        public bool HasCrown
        {
            get { return Host.PlayerService.CrownPlayer == this; }
        }

        public bool IsGuest
        {
            get { return PlayerUtils.IsGuest(Username); }
        }

        public string StorageName
        {
            get { return PlayerUtils.GetStorageName(Username); }
        }

        public string ChatName
        {
            get { return PlayerUtils.GetChatName(Username); }
        }

        public int BlockX
        {
            get { return BlockUtils.PosToBlock(PosX); }
        }

        public int BlockY
        {
            get { return BlockUtils.PosToBlock(PosY); }
        }

        protected override void Enable()
        {
            Metadata = new MetadataStore();

            ExtractHostData();

            BindPlayerEvent<AutoTextReceiveEvent, AutoTextPlayerEvent>(OnAutoText);
            BindPlayerEvent<CoinReceiveEvent, CoinPlayerEvent>(OnCoin);
            BindPlayerEvent<FaceReceiveEvent, FacePlayerEvent>(OnFace);
            BindPlayerEvent<MoveReceiveEvent, MovePlayerEvent>(OnMove);
            BindPlayerEvent<GodModeReceiveEvent, GodModePlayerEvent>(OnGodMode);
            BindPlayerEvent<ModModeReceiveEvent, ModModePlayerEvent>(OnModMode);
            BindPlayerEvent<SilverCrownReceiveEvent, SilverCrownPlayerEvent>(OnSilverCrown);
            BindPlayerEvent<LeftReceiveEvent, LeftPlayerEvent>(OnLeft);
            BindPlayerEvent<PotionReceiveEvent, PotionPlayerEvent>(OnPotion);
            BindPlayerEvent<SayReceiveEvent, SayPlayerEvent>(OnSay);
            BindPlayerEvent<LevelUpReceiveEvent, LevelUpPlayerEvent>(OnLevelUp);
            BindPlayerEvent<WootUpReceiveEvent, WootUpPlayerEvent>(OnWootUp);
            BindPlayerEvent<KillReceiveEvent, KillPlayerEvent>(OnKill);
            BindPlayerEvent<MagicReceiveEvent, MagicPlayerEvent>(OnMagic);
            BindPlayerEvent<CrownReceiveEvent, CrownPlayerEvent>(OnCrown);

            Events.Bind<TeleportUserReceiveEvent>(OnTeleportUser, EventPriority.High);
            Events.Bind<TeleportEveryoneReceiveEvent>(OnTeleportEveryone, EventPriority.High);
        }

        private void BindPlayerEvent<T, TPlayer>(EventHandler<T> callback)
            where T : Event, IUserReceiveEvent
            where TPlayer : PlayerEvent<T>
        {
            Events.Bind<T>(
                (sender, e) =>
                {
                    if (e.UserId == UserId)
                    {
                        callback(sender, e);
                        var instance = (TPlayer) Activator.CreateInstance(typeof (TPlayer),
                            BindingFlags.NonPublic |
                            BindingFlags.Instance,
                            null, new object[] {this, e}, null);

                        SynchronizePlatform.Do(() =>
                            Events.Raise(instance));
                    }
                }, EventPriority.High);
        }

        private void ExtractHostData()
        {
            var addArgs = Host as AddJoinArgs;
            if (addArgs != null)
            {
                AddReceiveEvent add = addArgs.AddReceiveEvent;

                UserId = add.UserId;
                Username = add.Username;
                Smiley = add.Face;
                HasChat = add.HasChat;
                IsGod = add.IsGod;
                IsMod = add.IsMod;
                IsMyFriend = add.IsMyFriend;
                Coins = add.Coins;
                PosX = add.UserPosX;
                PosY = add.UserPosY;
                SpawnX = add.UserPosX;
                SpawnY = add.UserPosY;
                IsClubMember = add.IsClubMember;
                MagicClass = add.MagicClass;
                JumpPotion = add.IsPurple;
            }

            var initArgs = Host as InitJoinArgs;
            if (initArgs != null)
            {
                InitReceiveEvent init = initArgs.InitReceiveEvent;

                UserId = init.UserId;
                Username = init.Username;
                PosX = init.SpawnX;
                PosY = init.SpawnY;
                SpawnX = init.SpawnX;
                SpawnY = init.SpawnY;
            }
        }

        private void OnAutoText(object sender, AutoTextReceiveEvent e)
        {
            AutoText = e.AutoText;
        }

        private void OnCoin(object sender, CoinReceiveEvent e)
        {
            Coins = e.Coins;
        }

        private void OnFace(object sender, FaceReceiveEvent e)
        {
            Smiley = e.Face;
        }

        private void OnMove(object sender, MoveReceiveEvent e)
        {
            Coins = e.Coins;
            Horizontal = e.Horizontal;
            Vertical = e.Vertical;
            ModifierX = e.ModifierX;
            ModifierY = e.ModifierY;
            SpeedX = e.SpeedX;
            SpeedY = e.SpeedY;
            PosX = e.UserPosX;
            PosY = e.UserPosY;
            SwitchOpened = e.IsPurple;
            IsDead = e.IsDead;
        }

        private void OnGodMode(object sender, GodModeReceiveEvent e)
        {
            IsGod = e.IsGod;
        }

        private void OnModMode(object sender, ModModeReceiveEvent e)
        {
            IsMod = true;
        }

        private void OnSilverCrown(object sender, SilverCrownReceiveEvent e)
        {
            HasSilverCrown = true;
        }

        private void OnLeft(object sender, LeftReceiveEvent e)
        {
            IsDisconnected = true;
        }

        private void OnPotion(object sender, PotionReceiveEvent e)
        {
            LastPotion = e.Potion;
            LastPotionEnabled = e.Enabled;
            LastPotionTimeout = e.Timeout;

            switch (e.Potion)
            {
                case Potion.RedAura:
                    RedAuraPotion = e.Enabled;
                    break;
                case Potion.BlueAura:
                    BlueAuraPotion = e.Enabled;
                    break;
                case Potion.YellowAura:
                    YellowAuraPotion = e.Enabled;
                    break;
                case Potion.GreenAura:
                    GreenAuraPotion = e.Enabled;
                    break;
                case Potion.Jump:
                    JumpPotion = e.Enabled;
                    break;
                case Potion.Fire:
                    FirePotion = e.Enabled;
                    break;
                case Potion.Curse:
                    CursePotion = e.Enabled;
                    break;
                case Potion.Protection:
                    ProtectionPotion = e.Enabled;
                    break;
                case Potion.Zombie:
                    ZombiePotion = e.Enabled;
                    break;
                case Potion.Respawn:
                    RespawnPotion = e.Enabled;
                    break;
                case Potion.Levitation:
                    LevitationPotion = e.Enabled;
                    break;
                case Potion.Flaunt:
                    FlauntPotion = e.Enabled;
                    break;
                case Potion.Solitude:
                    SolitudePotion = e.Enabled;
                    break;
            }
        }

        private void OnSay(object sender, SayReceiveEvent e)
        {
            Say = e.Text;
        }

        private void OnLevelUp(object sender, LevelUpReceiveEvent e)
        {
            MagicClass = e.NewClass;
        }

        private void OnWootUp(object sender, WootUpReceiveEvent e)
        {
            HasWooted = true;
        }

        private void OnMagic(object sender, MagicReceiveEvent e)
        {
            // Nothing to do here
        }

        private void OnKill(object sender, KillReceiveEvent e)
        {
            IsDead = true;
        }

        private void OnCrown(object sender, CrownReceiveEvent e)
        {
            // Nothing to do here
        }


        private void OnTeleportUser(object sender, TeleportUserReceiveEvent e)
        {
            if (UserId == e.UserId)
            {
                PosX = e.UserPosX;
                PosY = e.UserPosY;

                var point = new Point(PosX, PosY);
                SynchronizePlatform.Do(() =>
                    Events.Raise(new TeleportPlayerEvent(this, point)));
            }
        }

        private void OnTeleportEveryone(object sender, TeleportEveryoneReceiveEvent e)
        {
            if (e.Coordinates.ContainsKey(UserId))
            {
                Point location = e.Coordinates[UserId];
                PosX = location.X;
                PosY = location.Y;

                if (e.ResetCoins)
                {
                    Coins = default(int);
                }

                var point = new Point(PosX, PosY);
                SynchronizePlatform.Do(() =>
                    Events.Raise(new TeleportPlayerEvent(this, point)));
            }
        }
    }
}