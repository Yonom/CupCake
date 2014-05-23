using CupCake.Core.Events;
using CupCake.Core.Services;
using CupCake.EE;
using CupCake.EE.Events.Receive;
using CupCake.Room.Events;

namespace CupCake.Room.Services
{
    public class RoomService : CupCakeService
    {
        public string WorldName { get; private set; }
        public string Owner { get; private set; }
        public int Plays { get; private set; }
        public int CurrentWoots { get; private set; }
        public int TotalWoots { get; private set; }

        public AccessRight AccessRight { get; private set; }
        public string Encryption { get; private set; }

        public double GravityMultiplier { get; private set; }
        public bool AllowPotions { get; private set; }
        public bool IsTutorialRoom { get; private set; }

        public bool InitComplete { get; private set; }
        public bool JoinComplete { get; private set; }

        protected override void Enable()
        {
            this.Events.Bind<InitReceiveEvent>(this.OnInit, EventPriority.Lowest);
            this.Events.Bind<CrownReceiveEvent>(this.OnCrown, EventPriority.Lowest);
            this.Events.Bind<AccessReceiveEvent>(this.OnAccess, EventPriority.High);
            this.Events.Bind<LostAccessReceiveEvent>(this.OnLostAccess, EventPriority.High);
            this.Events.Bind<UpdateMetaReceiveEvent>(this.OnUpdateMeta, EventPriority.High);
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

            this.InitComplete = true;
            this.Events.Raise(new InitCompleteEvent());
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
                this.Events.Raise(new JoinCompleteEvent());
            }
        }

        private void OnUpdateMeta(object sender, UpdateMetaReceiveEvent e)
        {
            this.WorldName = e.WorldName;
            this.Plays = e.Plays;
            this.Owner = e.OwnerUsername;
            this.CurrentWoots = e.CurrentWoots;
            this.TotalWoots = e.TotalWoots;
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