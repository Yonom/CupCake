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
    public sealed class RoomService : CupCakeService
    {
        private AccessRight _accessRight;
        public string WorldName { get; private set; }
        public string Owner { get; private set; }
        public int Plays { get; private set; }
        public int CurrentWoots { get; private set; }
        public int TotalWoots { get; private set; }

        public AccessRight AccessRight
        {
            get { return _accessRight; }
            private set
            {
                if (AccessRight != value)
                {
                    _accessRight = value;

                    SynchronizePlatform.Do(() =>
                        Events.Raise(new AccessRoomEvent(_accessRight)));
                }
            }
        }

        public string Encryption { get; private set; }

        public double GravityMultiplier { get; private set; }
        public bool IsTutorialRoom { get; private set; }

        public bool InitComplete { get; private set; }
        public bool JoinComplete { get; private set; }

        protected override void Enable()
        {
            Events.Bind<InitReceiveEvent>(OnInit, EventPriority.High);
            Events.Bind<CrownReceiveEvent>(OnCrown, EventPriority.High);
            Events.Bind<AccessReceiveEvent>(OnAccess, EventPriority.High);
            Events.Bind<LostAccessReceiveEvent>(OnLostAccess, EventPriority.High);
            Events.Bind<UpdateMetaReceiveEvent>(OnUpdateMeta, EventPriority.High);

            Events.Bind<SendEvent>(OnSend, EventPriority.Highest);
        }

        public void Access(string roomKey)
        {
            if (AccessRight == AccessRight.None)
                throw new InvalidOperationException("You already have access.");

            Events.Raise(new AccessSendEvent(roomKey));
        }

        public void ChangeKey(string newKey)
        {
            if (AccessRight < AccessRight.Owner)
                throw new InvalidOperationException("Only owners are allowed to change key.");

            Events.Raise(new ChangeWorldEditKeySendEvent(newKey));
        }

        public void Clear()
        {
            if (AccessRight < AccessRight.Owner)
                throw new InvalidOperationException("Only owners are allowed to clear room.");

            Events.Raise(new ClearWorldSendEvent());
        }

        public void Save()
        {
            if (AccessRight < AccessRight.Owner)
                throw new InvalidOperationException("Only owners are allowed to save.");

            Events.Raise(new SaveWorldSendEvent());
        }

        public void SetAllowPotions(bool allowed)
        {
            if (AccessRight < AccessRight.Owner)
                throw new InvalidOperationException("Only owners are allowed to enable/disable potions.");

            Events.Raise(new AllowPotionsSendEvent(allowed));
        }

        public void SetName(string newName)
        {
            if (AccessRight < AccessRight.Owner)
                throw new InvalidOperationException("Only owners are allowed to change room name.");

            Events.Raise(new ChangeWorldNameSendEvent(newName));
        }

        public void KillRoom()
        {
            Events.Raise(new KillWorldSendEvent());
        }

        private void OnSend(object sender, SendEvent e)
        {
            var encryptedSend = e as IEncryptedSendEvent;
            if (encryptedSend != null && String.IsNullOrEmpty(encryptedSend.Encryption))
            {
                encryptedSend.Encryption = Encryption;
            }
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            Owner = e.OwnerUsername;
            WorldName = e.WorldName;
            Plays = e.Plays;
            Encryption = Rot13(e.Encryption);
            IsTutorialRoom = e.IsTutorialRoom;
            GravityMultiplier = e.Gravity;
            CurrentWoots = e.CurrentWoots;
            TotalWoots = e.TotalWoots;

            if (e.IsOwner)
            {
                AccessRight = AccessRight.Owner;
            }
            else if (e.CanEdit)
            {
                AccessRight = AccessRight.Edit;
            }

            RaiseMeta(e);

            if (!InitComplete)
            {
                InitComplete = true;
            }
        }

        private void OnAccess(object sender, AccessReceiveEvent e)
        {
            AccessRight = AccessRight.Edit;
        }

        private void OnLostAccess(object sender, LostAccessReceiveEvent e)
        {
            AccessRight = AccessRight.None;
        }

        private void OnCrown(object sender, CrownReceiveEvent e)
        {
            if (!JoinComplete)
            {
                JoinComplete = true;

                SynchronizePlatform.Do(() =>
                    Events.Raise(new JoinCompleteRoomEvent()));
            }
        }

        private void OnUpdateMeta(object sender, UpdateMetaReceiveEvent e)
        {
            WorldName = e.WorldName;
            Plays = e.Plays;
            Owner = e.OwnerUsername;
            CurrentWoots = e.CurrentWoots;
            TotalWoots = e.TotalWoots;

            RaiseMeta(e);
        }

        private void RaiseMeta(IMetadataReceiveMessage e)
        {
            SynchronizePlatform.Do(() =>
                Events.Raise(new MetaRoomEvent(e.OwnerUsername, e.Plays, e.CurrentWoots, e.TotalWoots, e.WorldName)));
        }

        private static string Rot13(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                var number = (int) array[i];

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
                array[i] = (char) number;
            }
            return new string(array);
        }
    }
}