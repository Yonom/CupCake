using System;
using System.Diagnostics;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Messages.Receive;
using CupCake.Messages.Send;
using CupCake.Messages.User;

namespace CupCake.Room
{
    [DebuggerDisplay("WorldName = {WorldName}, Owner = {Owner}, Plays = {Plays}")]
    public class RoomService : CupCakeService
    {
        private AccessRight _accessRight;
        public string WorldName { get; private set; }
        public string Owner { get; private set; }
        public int Plays { get; private set; }
        public int CurrentWoots { get; private set; }
        public int TotalWoots { get; private set; }

        public AccessRight AccessRight
        {
            get { return this._accessRight; }
            private set
            {
                if (this.AccessRight != value)
                {
                    this._accessRight = value;

                    this.SynchronizePlatform.Do(() =>
                        this.Events.Raise(new AccessRoomEvent(this._accessRight)));
                }
            }
        }

        public string Encryption { get; private set; }

        public double GravityMultiplier { get; private set; }
        public bool AllowPotions { get; private set; }
        public bool IsTutorialRoom { get; private set; }

        public bool InitComplete { get; private set; }
        public bool JoinComplete { get; private set; }

        protected override void Enable()
        {
            this.Events.Bind<InitReceiveEvent>(this.OnInit, EventPriority.High);
            this.Events.Bind<CrownReceiveEvent>(this.OnCrown, EventPriority.High);
            this.Events.Bind<AccessReceiveEvent>(this.OnAccess, EventPriority.High);
            this.Events.Bind<LostAccessReceiveEvent>(this.OnLostAccess, EventPriority.High);
            this.Events.Bind<UpdateMetaReceiveEvent>(this.OnUpdateMeta, EventPriority.High);
            this.Events.Bind<AllowPotionsReceiveEvent>(this.OnAllowPotions, EventPriority.High);

            this.Events.Bind<SendEvent>(this.OnSend, EventPriority.Highest);
        }

        public void Access(string roomKey)
        {
            if (this.AccessRight == AccessRight.None)
                throw new InvalidOperationException("You already have access.");

            this.Events.Raise(new AccessSendEvent(roomKey));
        }

        public void ChangeKey(string newKey)
        {
            if (this.AccessRight < AccessRight.Owner)
                throw new InvalidOperationException("Only owners are allowed to change key.");

            this.Events.Raise(new ChangeWorldEditKeySendEvent(newKey));
        }

        public void Clear()
        {
            if (this.AccessRight < AccessRight.Owner)
                throw new InvalidOperationException("Only owners are allowed to clear room.");

            this.Events.Raise(new ClearWorldSendEvent());
        }

        public void Save()
        {
            if (this.AccessRight < AccessRight.Owner)
                throw new InvalidOperationException("Only owners are allowed to save.");

            this.Events.Raise(new SaveWorldSendEvent());
        }

        public void SetAllowPotions(bool allowed)
        {
            if (this.AccessRight < AccessRight.Owner)
                throw new InvalidOperationException("Only owners are allowed to enable/disable potions.");

            this.Events.Raise(new AllowPotionsSendEvent(allowed));
        }

        public void SetName(string newName)
        {
            if (this.AccessRight < AccessRight.Owner)
                throw new InvalidOperationException("Only owners are allowed to change room name.");

            this.Events.Raise(new ChangeWorldNameSendEvent(newName));
        }

        public void KillRoom()
        {
            this.Events.Raise(new KillWorldSendEvent());
        }

        private void OnSend(object sender, SendEvent e)
        {
            var encryptedSend = e as IEncryptedSendEvent;
            if (encryptedSend != null && String.IsNullOrEmpty(encryptedSend.Encryption))
            {
                encryptedSend.Encryption = this.Encryption;
            }
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            this.Owner = e.OwnerUsername;
            this.WorldName = e.WorldName;
            this.Plays = e.Plays;
            this.Encryption = Rot13(e.Encryption);
            this.IsTutorialRoom = e.IsTutorialRoom;
            this.GravityMultiplier = e.Gravity;
            this.AllowPotions = e.AllowPotions;
            this.CurrentWoots = e.CurrentWoots;
            this.TotalWoots = e.TotalWoots;

            if (e.IsOwner)
            {
                this.AccessRight = AccessRight.Owner;
            }
            else if (e.CanEdit)
            {
                this.AccessRight = AccessRight.Edit;
            }

            this.RaiseMeta(e);

            if (!this.InitComplete)
            {
                this.InitComplete = true;
            }
        }

        private void OnAccess(object sender, AccessReceiveEvent e)
        {
            this.AccessRight = AccessRight.Edit;
        }

        private void OnLostAccess(object sender, LostAccessReceiveEvent e)
        {
            this.AccessRight = AccessRight.None;
        }

        private void OnCrown(object sender, CrownReceiveEvent e)
        {
            if (!this.JoinComplete)
            {
                this.JoinComplete = true;

                this.SynchronizePlatform.Do(() =>
                    this.Events.Raise(new JoinCompleteRoomEvent()));
            }
        }

        private void OnUpdateMeta(object sender, UpdateMetaReceiveEvent e)
        {
            this.WorldName = e.WorldName;
            this.Plays = e.Plays;
            this.Owner = e.OwnerUsername;
            this.CurrentWoots = e.CurrentWoots;
            this.TotalWoots = e.TotalWoots;

            this.RaiseMeta(e);
        }

        private void OnAllowPotions(object sender, AllowPotionsReceiveEvent e)
        {
            this.AllowPotions = e.Allowed;
        }

        private void RaiseMeta(IMetadataReceiveMessage e)
        {
            this.SynchronizePlatform.Do(() =>
                this.Events.Raise(new MetaRoomEvent(e.OwnerUsername, e.Plays, e.CurrentWoots, e.TotalWoots, e.WorldName)));
        }

        private static string Rot13(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                var number = (int)array[i];

                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                array[i] = (char)number;
            }
            return new string(array);
        }
    }
}