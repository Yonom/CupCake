using System;
using System.Collections.Generic;
using System.Linq;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Messages.Receive;
using CupCake.Messages.Send;
using CupCake.Messages.User;

namespace CupCake.Potions
{
    public sealed class PotionService : CupCakeService
    {
        private readonly Dictionary<Potion, int> _potionCounts = new Dictionary<Potion, int>();

        public bool AllowPotions { get; private set; }

        public Potion[] DisabledPotions { get; private set; }

        public int GetCount(Potion potion)
        {
            lock (this._potionCounts)
            {
                if (!this._potionCounts.ContainsKey(potion))
                    return new int();

                return this._potionCounts[potion];
            }
        }

        private void SetPotion(Potion potion, int value)
        {
            lock (this._potionCounts)
            {
                this._potionCounts[potion] = value;

                this.SynchronizePlatform.Do(() =>
                    this.Events.Raise(new PotionCountEvent(potion, value)));
            }
        }

        public void UsePotion(Potion pot)
        {
            if (!this.AllowPotions)
                throw new InvalidOperationException("Potions have been disabled in this world!");
            if (this.DisabledPotions.Contains(pot))
                throw new InvalidOperationException("That potion has been disabled in this world!");
            if (this.GetCount(pot) == 0)
                throw new InvalidOperationException("Bot does not own any potions of that type!");

            this.Events.Raise(new PotionSendEvent(pot));
        }

        protected override void Enable()
        {
            this.Events.Bind<InitReceiveEvent>(this.OnInit, EventPriority.High);
            this.Events.Bind<PotionCountReceiveEvent>(this.OnPotionCount, EventPriority.High);
            this.Events.Bind<AllowPotionsReceiveEvent>(this.OnAllowPotions, EventPriority.High);
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            this.AllowPotions = e.AllowPotions;
            this.OnPotionCount(sender, e);
        }

        private void OnPotionCount(object sender, ReceiveEvent e)
        {
            uint startNum = 0;
            for (int i = Convert.ToInt32(e.PlayerIOMessage.Count - 1u); i >= 0; i += -1)
            {
                if (e.PlayerIOMessage[Convert.ToUInt32(i)] as string != null &&
                    e.PlayerIOMessage.GetString(Convert.ToUInt32(i)) == "pe")
                {
                    startNum = Convert.ToUInt32(i - 1);
                }
            }

            uint pointer = startNum;
            while (e.PlayerIOMessage[pointer] as string == null || e.PlayerIOMessage.GetString(pointer) != "ps")
            {
                this.SetPotion(
                    ((Potion)e.PlayerIOMessage.GetInteger(pointer - 1)),
                    e.PlayerIOMessage.GetInteger(pointer));
                pointer -= 2;
            }
        }

        private void OnAllowPotions(object sender, AllowPotionsReceiveEvent e)
        {
            this.AllowPotions = e.Allowed;
            this.DisabledPotions = e.DisabledPotions;
        }
    }
}