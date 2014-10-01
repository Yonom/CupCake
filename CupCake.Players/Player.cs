using System;
using System.Diagnostics;
using System.Reflection;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Core.Metadata;
using CupCake.Messages.Blocks;
using CupCake.Messages.Receive;
using CupCake.Messages.User;
using CupCake.Players.Join;

namespace CupCake.Players
{
    [DebuggerDisplay("Username = {Username}, Smiley = {Smiley}")]
    public sealed class Player : MetadataServicePart<JoinArgs>
    {
        protected override object MetadataKey
        {
            get { return this.UserId; }
        }

        /// <summary>
        /// Gets the player's username.
        /// </summary>
        /// <value>
        /// The player's username.
        /// </value>
        public string Username { get; private set; }

        /// <summary>
        /// Gets the player's user identifier.
        /// </summary>
        /// <value>
        /// The player's user identifier.
        /// </value>
        public int UserId { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this player has god mode enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this this player has god mode enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsGod { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this player has guardian mode enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this player has guardian mode enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsGuardian { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this player has moderator mode enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this player has moderator mode enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsMod { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this player is dead.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this player is dead; otherwise, <c>false</c>.
        /// </value>
        public bool IsDead { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this player is the bot user's friend.
        /// </summary>
        /// <value>
        /// <c>true</c> if this player is the bot user's friend; otherwise, <c>false</c>.
        /// </value>
        public bool IsMyFriend { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this player is a Builder's Club member.
        /// </summary>
        /// <value>
        /// <c>true</c> if this player is a Builder's Club member; otherwise, <c>false</c>.
        /// </value>
        public bool IsClubMember { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this player is disconnected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this player is disconnected; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisconnected { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this player has chat access.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this player has chat access; otherwise, <c>false</c>.
        /// </value>
        public bool HasChat { get; private set; }

        /// <summary>
        /// Gets the player's magic class.
        /// </summary>
        /// <value>
        /// The player's magic class.
        /// </value>
        public MagicClass MagicClass { get; private set; }

        /// <summary>
        /// Gets the player's smiley.
        /// </summary>
        /// <value>
        /// The player's smiley.
        /// </value>
        public Smiley Smiley { get; private set; }

        /// <summary>
        /// Gets the player's number of coins.
        /// </summary>
        /// <value>
        /// The player's number of coins.
        /// </value>
        public int Coins { get; private set; }

        /// <summary>
        /// Gets the player's number of blue coins.
        /// </summary>
        /// <value>
        /// The player's number of blue coins.
        /// </value>
        public int BlueCoins { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player has opened a switch.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player has opened a switch; otherwise, <c>false</c>.
        /// </value>
        public bool SwitchOpened { get; private set; }

        /// <summary>
        /// Gets the x-coordinate of the player's spawn.
        /// </summary>
        /// <value>
        /// The x-coordinate of the player's spawn.
        /// </value>
        public int SpawnX { get; private set; }

        /// <summary>
        /// Gets the y-coordinate of the player's spawn.
        /// </summary>
        /// <value>
        /// The y-coordinate of the player's spawn.
        /// </value>
        public int SpawnY { get; private set; }

        /// <summary>
        /// Gets the x-coordinate of the player's current position.
        /// </summary>
        /// <value>
        /// The x-coordinate of the player's current position.
        /// </value>
        public int PosX { get; private set; }

        /// <summary>
        /// Gets the y-coordinate of the player's current position.
        /// </summary>
        /// <value>
        /// The y-coordinate of the player's current position.
        /// </value>
        public int PosY { get; private set; }

        /// <summary>
        /// Gets the player's horizontal speed.
        /// </summary>
        /// <value>
        /// The player's horizontal speed.
        /// </value>
        public double SpeedX { get; private set; }

        /// <summary>
        /// Gets the player's vertical speed.
        /// </summary>
        /// <value>
        /// The player's vertical speed
        /// </value>
        public double SpeedY { get; private set; }

        /// <summary>
        /// Gets the player's horizontal speed modifier.
        /// </summary>
        /// <value>
        /// The player's horizontal speed modifier.
        /// </value>
        public double ModifierX { get; private set; }

        /// <summary>
        /// Gets the player's vertical speed modifier.
        /// </summary>
        /// <value>
        /// The player's vertical speed modifier.
        /// </value>
        public double ModifierY { get; private set; }

        /// <summary>
        /// Gets the player's horizontal speed direction.
        /// </summary>
        /// <value>
        /// The player's horizontal speed direction.
        /// </value>
        public double Horizontal { get; private set; }

        /// <summary>
        /// Gets the player's vertical speed direction.
        /// </summary>
        /// <value>
        /// The player's vertical speed direction.
        /// </value>
        public double Vertical { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is pressing spacebar.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is pressing spacebar; otherwise, <c>false</c>.
        /// </value>
        public bool SpaceDown { get; set; }

        /// <summary>
        /// Gets the player's latest regular chat message.
        /// </summary>
        /// <value>
        /// The player's latest regular chat message.
        /// </value>
        public string Say { get; private set; }

        /// <summary>
        /// Gets the player's latest AutoText chat message.
        /// </summary>
        /// <value>
        /// The player's latest AutoText chat message.
        /// </value>
        public string AutoText { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this player has a silver crown.
        /// </summary>
        /// <value>
        /// <c>true</c> if this player has a silver crown; otherwise, <c>false</c>.
        /// </value>
        public bool HasSilverCrown { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this player has wooted the level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this player has wooted the level; otherwise, <c>false</c>.
        /// </value>
        public bool HasWooted { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is using a red aura potion.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is using a red aura potion; otherwise, <c>false</c>.
        /// </value>
        public bool RedAuraPotion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is using a blue aura potion.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is using a blue aura potion; otherwise, <c>false</c>.
        /// </value>
        public bool BlueAuraPotion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is using a yellow aura potion.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is using a yellow aura potion; otherwise, <c>false</c>.
        /// </value>
        public bool YellowAuraPotion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is using a green aura potion.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is using a green aura potion; otherwise, <c>false</c>.
        /// </value>
        public bool GreenAuraPotion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is using a jump potion.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is using a jump potion; otherwise, <c>false</c>.
        /// </value>
        public bool JumpPotion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is using a fire potion.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is using a fire potion; otherwise, <c>false</c>.
        /// </value>
        public bool FirePotion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is using a curse potion.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is using a curse potion; otherwise, <c>false</c>.
        /// </value>
        public bool CursePotion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is using a protection potion.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is using a protection potion; otherwise, <c>false</c>.
        /// </value>
        public bool ProtectionPotion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is using a zombie potion.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is using a zombie potion; otherwise, <c>false</c>.
        /// </value>
        public bool ZombiePotion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is using a respawn potion.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is using a respawn potion; otherwise, <c>false</c>.
        /// </value>
        public bool RespawnPotion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is using a levitation potion.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is using a levitation potion; otherwise, <c>false</c>.
        /// </value>
        public bool LevitationPotion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is using a flaunt potion.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is using a flaunt potion; otherwise, <c>false</c>.
        /// </value>
        public bool FlauntPotion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is using a solitude potion.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player is using a solitude potion; otherwise, <c>false</c>.
        /// </value>
        public bool SolitudePotion { get; private set; }

        /// <summary>
        /// Gets the player's last used potion.
        /// </summary>
        /// <value>
        /// The player's last used potion.
        /// </value>
        public Potion LastPotion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player's last potion was enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the player's last potion was enabled; otherwise, <c>false</c>.
        /// </value>
        public bool LastPotionEnabled { get; private set; }

        /// <summary>
        /// Gets the timeout for the player's last potion.
        /// </summary>
        /// <value>
        /// The timeout for the player's last potion.
        /// </value>
        public int LastPotionTimeout { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this player has a crown.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this player has a crown; otherwise, <c>false</c>.
        /// </value>
        public bool HasCrown
        {
            get { return this.Host.PlayerService.CrownPlayer == this; }
        }

        /// <summary>
        /// Gets a value indicating whether this player is flying using god mode, guardian mode or moderator mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this player is flying using god mode, guardian mode or moderator mode; otherwise, <c>false</c>.
        /// </value>
        public bool IsFlying
        {
            get { return this.IsGod || this.IsGuardian || this.IsMod; }
        }

        /// <summary>
        /// Gets a value indicating whether this player is a guest.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this player is a guest; otherwise, <c>false</c>.
        /// </value>
        public bool IsGuest
        {
            get { return PlayerUtils.IsGuest(this.Username); }
        }

        /// <summary>
        /// Gets the player's storage name.
        /// </summary>
        /// <value>
        /// The player's storage name.
        /// </value>
        public string StorageName
        {
            get { return PlayerUtils.GetStorageName(this.Username); }
        }

        /// <summary>
        /// Gets the player's chat name.
        /// </summary>
        /// <value>
        /// The player's chat name.
        /// </value>
        public string ChatName
        {
            get { return PlayerUtils.GetChatName(this.Username); }
        }

        /// <summary>
        /// Gets the x-coordinate of the block that the player is located on.
        /// </summary>
        /// <value>
        /// The x-coordinate of the block that the player is located on.
        /// </value>
        public int BlockX
        {
            get { return BlockUtils.PosToBlock(this.PosX); }
        }

        /// <summary>
        /// Gets the y-coordinate of the block that the player is located on.
        /// </summary>
        /// <value>
        /// The y-coordinate of the block that the player is located on.
        /// </value>
        public int BlockY
        {
            get { return BlockUtils.PosToBlock(this.PosY); }
        }

        protected override void Enable()
        {
            this.ExtractHostData();

            this.BindPlayerEvent<AutoTextReceiveEvent, AutoTextPlayerEvent>(this.OnAutoText);
            this.BindPlayerEvent<CoinReceiveEvent, CoinPlayerEvent>(this.OnCoin);
            this.BindPlayerEvent<FaceReceiveEvent, FacePlayerEvent>(this.OnFace);
            this.BindPlayerEvent<MoveReceiveEvent, MovePlayerEvent>(this.OnMove);
            this.BindPlayerEvent<GodModeReceiveEvent, GodModePlayerEvent>(this.OnGodMode);
            this.BindPlayerEvent<GuardianModeReceiveEvent, GuardianModePlayerEvent>(this.OnGuardianMode);
            this.BindPlayerEvent<ModModeReceiveEvent, ModModePlayerEvent>(this.OnModMode);
            this.BindPlayerEvent<SilverCrownReceiveEvent, SilverCrownPlayerEvent>(this.OnSilverCrown);
            this.BindPlayerEvent<LeftReceiveEvent, LeftPlayerEvent>(this.OnLeft);
            this.BindPlayerEvent<PotionReceiveEvent, PotionPlayerEvent>(this.OnPotion);
            this.BindPlayerEvent<SayReceiveEvent, SayPlayerEvent>(this.OnSay);
            this.BindPlayerEvent<LevelUpReceiveEvent, LevelUpPlayerEvent>(this.OnLevelUp);
            this.BindPlayerEvent<WootUpReceiveEvent, WootUpPlayerEvent>(this.OnWootUp);
            this.BindPlayerEvent<KillReceiveEvent, KillPlayerEvent>(this.OnKill);
            this.BindPlayerEvent<MagicReceiveEvent, MagicPlayerEvent>(this.OnMagic);
            this.BindPlayerEvent<CrownReceiveEvent, CrownPlayerEvent>(this.OnCrown);

            this.Events.Bind<TeleportUserReceiveEvent>(this.OnTeleportUser, EventPriority.High);
            this.Events.Bind<TeleportEveryoneReceiveEvent>(this.OnTeleportEveryone, EventPriority.High);
        }

        private void BindPlayerEvent<T, TPlayer>(EventHandler<T> callback)
            where T : Event, IUserReceiveEvent
            where TPlayer : PlayerEvent<T>
        {
            this.Events.Bind<T>(
                (sender, e) =>
                {
                    if (e.UserId == this.UserId)
                    {
                        var copy = (Player)this.MemberwiseClone();

                        callback(sender, e);
                        this.RaisePlayerEvent<T, TPlayer>(copy, e);
                    }
                }, EventPriority.High);
        }


        internal void RaisePlayerEvent<T, TPlayer>(Player old, T e) where TPlayer : PlayerEvent<T>
        {
            var instance = (TPlayer)Activator.CreateInstance(typeof(TPlayer),
                BindingFlags.NonPublic |
                BindingFlags.Instance,
                null, new object[] { old, this, e }, null);

            this.SynchronizePlatform.Do(() =>
                this.Events.Raise(instance));
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
                this.IsGuardian = add.IsGuardian;
                this.IsMod = add.IsMod;
                this.IsMyFriend = add.IsMyFriend;
                this.Coins = add.Coins;
                this.BlueCoins = add.BlueCoins;
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
            this.BlueCoins = e.BlueCoins;
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
            this.IsDead = false;
            this.SwitchOpened = e.IsPurple;
            this.SpaceDown = e.SpaceDown;
        }

        private void OnGodMode(object sender, GodModeReceiveEvent e)
        {
            this.IsGod = e.IsGod;
        }

        private void OnGuardianMode(object sender, GuardianModeReceiveEvent e)
        {
            this.IsGuardian = e.IsGuardian;
        }

        private void OnModMode(object sender, ModModeReceiveEvent e)
        {
            this.IsMod = true;
        }

        private void OnSilverCrown(object sender, SilverCrownReceiveEvent e)
        {
            this.HasSilverCrown = true;
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
            this.IsDead = true;
        }

        private void OnCrown(object sender, CrownReceiveEvent e)
        {
            // Nothing to do here
        }

        private void OnTeleportUser(object sender, TeleportUserReceiveEvent e)
        {
            if (this.UserId == e.UserId)
            {
                var copy = (Player)this.MemberwiseClone();

                this.PosX = e.UserPosX;
                this.PosY = e.UserPosY;
                this.IsDead = false;

                var point = new Point(this.PosX, this.PosY);
                this.RaisePlayerEvent<Point, TeleportPlayerEvent>(copy, point);
            }
        }

        private void OnTeleportEveryone(object sender, TeleportEveryoneReceiveEvent e)
        {
            if (e.Coordinates.ContainsKey(this.UserId))
            {
                var copy = (Player)this.MemberwiseClone();

                Point location = e.Coordinates[this.UserId];
                this.PosX = location.X;
                this.PosY = location.Y;
                this.SpawnX = location.X;
                this.SpawnY = location.Y;

                this.IsDead = false;

                if (e.ResetCoins)
                {
                    this.Coins = default(int);
                }

                var point = new Point(this.PosX, this.PosY);
                this.RaisePlayerEvent<Point, TeleportPlayerEvent>(copy, point);
            }
        }
    }
}