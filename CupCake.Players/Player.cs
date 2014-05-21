﻿using System;
using CupCake.Core.Events;
using CupCake.Core.Services;
using CupCake.EE.Blocks;
using CupCake.EE.Events;
using CupCake.EE.Events.Receive;
using CupCake.EE.User;
using CupCake.Players.Join;
using CupCake.Players.Services;

namespace CupCake.Players
{
    public class Player : CupCakeServicePart<JoinArgs>
    {
        public string Username { get; private set; }
        public int UserId { get; private set; }

        public string DatabaseName { get; private set; }
        public bool IsUserDataReady { get; private set; }
        public bool IsGuest { get; private set; }
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
        public int PlayerPosX { get; private set; }
        public int PlayerPosY { get; private set; }
        public int BlockX { get; private set; }
        public int BlockY { get; private set; }
        public double SpeedX { get; private set; }
        public double SpeedY { get; private set; }
        public double ModifierX { get; private set; }
        public double ModifierY { get; private set; }
        public double Vertical { get; private set; }

        public double Horizontal { get; private set; }
        public string Say { get; private set; }

        public string AutoText { get; private set; }
        public bool HasCrown { get; private set; }

        public bool HasSilverCrown { get; private set; }
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
        public Group Group { get; private set; }

        protected override void Enable()
        {
            this.ExtractHostData();


        }

        private void BindWithId<T>(EventHandler<T> callback) where T : Event, IUserEvent
        {
            this.Events.Bind<T>(
                (sender, e) =>
                {
                    if (e.UserId == this.UserId)
                    {
                        callback(sender, e);
                    }
                }, EventPriority.BeforeMost);
        }

        private void ExtractHostData()
        {
            var addArgs = this.Host as AddJoinArgs;
            if (addArgs != null)
            {
                var add = addArgs.AddReceiveEvent;

                this.UserId = add.UserId;
                this.Username = add.Username;
                this.Smiley = add.Face;
                this.HasChat = add.HasChat;
                this.IsGod = add.IsGod;
                this.IsMod = add.IsMod;
                this.IsMyFriend = add.IsMyFriend;
                this.Coins = add.Coins;
                this.PlayerPosX = add.UserPosX;
                this.PlayerPosY = add.UserPosY;
                this.SpawnX = add.UserPosX;
                this.SpawnY = add.UserPosY;
                this.IsClubMember = add.IsClubMember;
                this.MagicClass = add.MagicClass;
                this.JumpPotion = add.IsPurple;
            }

            var initArgs = this.Host as InitJoinArgs;
            if (initArgs != null)
            {
                var init = initArgs.InitReceiveEvent;

                this.UserId = init.UserId;
                this.Username = init.Username;
                this.PlayerPosX = init.SpawnX;
                this.PlayerPosY = init.SpawnY;
                this.SpawnX = init.SpawnX;
                this.SpawnY = init.SpawnY;
            }
        }


    }
}