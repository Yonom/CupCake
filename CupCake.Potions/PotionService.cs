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
            lock (_potionCounts)
            {
                if (!_potionCounts.ContainsKey(potion))
                    return new int();

                return _potionCounts[potion];
            }
        }

        private void SetPotion(Potion potion, int value)
        {
            lock (_potionCounts)
            {
                _potionCounts[potion] = value;

                SynchronizePlatform.Do(() =>
                    Events.Raise(new PotionCountEvent(potion, value)));
            }
        }

        public void UsePotion(Potion pot)
        {
            if (!AllowPotions)
                throw new InvalidOperationException("Potions have been disabled in this world!");
            if (DisabledPotions.Contains(pot))
                throw new InvalidOperationException("That potion has been disabled in this world!");
            if (GetCount(pot) == 0)
                throw new InvalidOperationException("Bot does not own any potions of that type!");

            Events.Raise(new PotionSendEvent(pot));
        }

        protected override void Enable()
        {
            Events.Bind<InitReceiveEvent>(OnInit, EventPriority.High);
            Events.Bind<PotionCountReceiveEvent>(OnPotionCount, EventPriority.High);
            Events.Bind<AllowPotionsReceiveEvent>(OnAllowPotions, EventPriority.High);
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            AllowPotions = e.AllowPotions;
            OnPotionCount(sender, e);
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
                SetPotion(
                    ((Potion) e.PlayerIOMessage.GetInteger(pointer - 1)),
                    e.PlayerIOMessage.GetInteger(pointer));
                pointer -= 2;
            }
        }

        private void OnAllowPotions(object sender, AllowPotionsReceiveEvent e)
        {
            AllowPotions = e.Allowed;
            DisabledPotions = e.DisabledPotions;
        }
    }
}