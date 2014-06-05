using System;
using System.Diagnostics;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Messages;
using CupCake.Messages.Blocks;
using CupCake.Messages.Receive;
using CupCake.Messages.User;
using CupCake.Players.Join;
using CupCake.Players.Metadata;

namespace CupCake.Players
{
    [DebuggerDisplay("Username = {Username}, Smiley = {Smiley}")]
    public class Player : CupCakeServicePart<JoinArgs>
    {
        public MetadataManager Metadata { get; private set; }

        public string Username { get; private set; }
        public int UserId { get; private set; }

        public bool IsGod { get; private set; }
        public bool IsMod { get; private set; }
        public bool IsMyFriend { get; private set; }
        public bool IsClubMember { get; private set; }
        public bool IsDisconnected { get; private set; }

        public bool HasChat { get; private set; }
        public MagicClass MagicClass { get; private set; }
        public Smiley Smiley { get; private set; }

        public int Coins { get; private set; }
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
            get { return this.Host.PlayerService.CrownPlayer == this; }
        }

        public bool IsGuest
        {
            get
            {
                // Offcial implmentation in SWF, don't blame me
                return this.Username.Contains("-");
            }
        }

        public int BlockX
        {
            get { return this.PosX + 8 >> 4; }
        }

        public int BlockY
        {
            get { return this.PosY + 8 >> 4; }
        }

        protected override void Enable()
        {
            this.Metadata = new MetadataManager();

            this.ExtractHostData();

            this.BindPlayerEvent<AutoTextReceiveEvent, AutoTextPlayerEvent>(this.OnAutoText);
            this.BindPlayerEvent<CoinReceiveEvent, CoinPlayerEvent>(this.OnCoin);
            this.BindPlayerEvent<FaceReceiveEvent, FacePlayerEvent>(this.OnFace);
            this.BindPlayerEvent<MoveReceiveEvent, MovePlayerEvent>(this.OnMove);
            this.BindPlayerEvent<GodModeReceiveEvent, GodModePlayerEvent>(this.OnGodMode);
            this.BindPlayerEvent<ModModeReceiveEvent, ModModePlayerEvent>(this.OnModMode);
            this.BindPlayerEvent<SilverCrownReceiveEvent, SilverCrownPlayerEvent>(this.OnSilverCrown);
            this.BindPlayerEvent<TeleportUserReceiveEvent, TeleportPlayerEvent>(this.OnTeleportUser);
            this.BindPlayerEvent<LeftReceiveEvent, LeftPlayerEvent>(this.OnLeft);
            this.BindPlayerEvent<PotionReceiveEvent, PotionPlayerEvent>(this.OnPotion);
            this.BindPlayerEvent<SayReceiveEvent, SayPlayerEvent>(this.OnSay);
            this.BindPlayerEvent<LevelUpReceiveEvent, LevelUpPlayerEvent>(this.OnLevelUp);
            this.BindPlayerEvent<WootUpReceiveEvent, WootUpPlayerEvent>(this.OnWootUp);
            this.BindPlayerEvent<KillReceiveEvent, KillPlayerEvent>(this.OnKill);
            this.BindPlayerEvent<MagicReceiveEvent, MagicPlayerEvent>(this.OnMagic);
            this.BindPlayerEvent<CrownReceiveEvent, CrownPlayerEvent>(this.OnCrown);

            this.Events.Bind<TeleportEveryoneReceiveEvent>(this.OnTeleportEveryone, EventPriority.High);
        }

        private void BindPlayerEvent<T, TPlayer>(EventHandler<T> callback)
            where T : Event, IUserEvent
            where TPlayer : PlayerEvent<T>
        {
            this.Events.Bind<T>(
                (sender, e) =>
                {
                    if (e.UserId == this.UserId)
                    {
                        callback(sender, e);

                        var instance = (TPlayer)Activator.CreateInstance(typeof(TPlayer), this, e);
                        this.Events.Raise(instance);
                    }
                }, EventPriority.Lowest);
        }

        private void ExtractHostData()
        {
            var addArgs = this.Host as AddJoinArgs;
            if (addArgs != null)
            {
                AddReceiveEvent add = addArgs.AddReceiveEvent;

                this.UserId = add.UserId;
                this.Username = add.Username;
                this.Smiley = add.Face;
                this.HasChat = add.HasChat;
                this.IsGod = add.IsGod;
                this.IsMod = add.IsMod;
                this.IsMyFriend = add.IsMyFriend;
                this.Coins = add.Coins;
                this.PosX = add.UserPosX;
                this.PosY = add.UserPosY;
                this.SpawnX = add.UserPosX;
                this.SpawnY = add.UserPosY;
                this.IsClubMember = add.IsClubMember;
                this.MagicClass = add.MagicClass;
                this.JumpPotion = add.IsPurple;
            }

            var initArgs = this.Host as InitJoinArgs;
            if (initArgs != null)
            {
                InitReceiveEvent init = initArgs.InitReceiveEvent;

                this.UserId = init.UserId;
                this.Username = init.Username;
                this.PosX = init.SpawnX;
                this.PosY = init.SpawnY;
                this.SpawnX = init.SpawnX;
                this.SpawnY = init.SpawnY;
            }
        }

        private void OnAutoText(object sender, AutoTextReceiveEvent e)
        {
            this.AutoText = e.AutoText;
        }

        private void OnCoin(object sender, CoinReceiveEvent e)
        {
            this.Coins = e.Coins;
        }

        private void OnFace(object sender, FaceReceiveEvent e)
        {
            this.Smiley = e.Face;
        }

        private void OnMove(object sender, MoveReceiveEvent e)
        {
            this.Coins = e.Coins;
            this.Horizontal = e.Horizontal;
            this.Vertical = e.Vertical;
            this.ModifierX = e.ModifierX;
            this.ModifierY = e.ModifierY;
            this.SpeedX = e.SpeedX;
            this.SpeedY = e.SpeedY;
            this.PosX = e.UserPosX;
            this.PosY = e.UserPosY;
            this.JumpPotion = e.IsPurple;
        }

        private void OnGodMode(object sender, GodModeReceiveEvent e)
        {
            this.IsGod = e.IsGod;
        }

        private void OnModMode(object sender, ModModeReceiveEvent e)
        {
            this.IsMod = true;
        }

        private void OnSilverCrown(object sender, SilverCrownReceiveEvent e)
        {
            this.HasSilverCrown = true;
        }

        private void OnTeleportUser(object sender, TeleportUserReceiveEvent e)
        {
            this.PosX = e.UserPosX;
            this.PosY = e.UserPosY;
        }

        private void OnLeft(object sender, LeftReceiveEvent e)
        {
            this.IsDisconnected = true;
        }

        private void OnPotion(object sender, PotionReceiveEvent e)
        {
            this.LastPotion = e.Potion;
            this.LastPotionEnabled = e.Enabled;
            this.LastPotionTimeout = e.Timeout;

            switch (e.Potion)
            {
                case Potion.RedAura:
                    this.RedAuraPotion = e.Enabled;
                    break;
                case Potion.BlueAura:
                    this.BlueAuraPotion = e.Enabled;
                    break;
                case Potion.YellowAura:
                    this.YellowAuraPotion = e.Enabled;
                    break;
                case Potion.GreenAura:
                    this.GreenAuraPotion = e.Enabled;
                    break;
                case Potion.Jump:
                    this.JumpPotion = e.Enabled;
                    break;
                case Potion.Fire:
                    this.FirePotion = e.Enabled;
                    break;
                case Potion.Curse:
                    this.CursePotion = e.Enabled;
                    break;
                case Potion.Protection:
                    this.ProtectionPotion = e.Enabled;
                    break;
                case Potion.Zombie:
                    this.ZombiePotion = e.Enabled;
                    break;
                case Potion.Respawn:
                    this.RespawnPotion = e.Enabled;
                    break;
                case Potion.Levitation:
                    this.LevitationPotion = e.Enabled;
                    break;
                case Potion.Flaunt:
                    this.FlauntPotion = e.Enabled;
                    break;
                case Potion.Solitude:
                    this.SolitudePotion = e.Enabled;
                    break;
            }
        }

        private void OnSay(object sender, SayReceiveEvent e)
        {
            this.Say = e.Text;
        }

        private void OnLevelUp(object sender, LevelUpReceiveEvent e)
        {
            this.MagicClass = e.NewClass;
        }

        private void OnWootUp(object sender, WootUpReceiveEvent e)
        {
            this.HasWooted = true;
        }

        private void OnMagic(object sender, MagicReceiveEvent e)
        {
            // Nothing to do here
        }

        private void OnKill(object sender, KillReceiveEvent e)
        {
            // Nothing to do here
        }

        private void OnCrown(object sender, CrownReceiveEvent e)
        {
            // Nothing to do here
        }

        private void OnTeleportEveryone(object sender, TeleportEveryoneReceiveEvent e)
        {
            if (e.Coordinates.ContainsKey(this.UserId))
            {
                Point location = e.Coordinates[this.UserId];
                this.PosX = location.X;
                this.PosY = location.Y;

                if (e.ResetCoins)
                {
                    this.Coins = default(int);
                }
            }
        }
    }
}