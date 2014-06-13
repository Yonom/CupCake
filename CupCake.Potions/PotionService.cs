using System;
using System.Collections.Generic;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Messages;
using CupCake.Messages.Blocks;
using CupCake.Messages.Receive;
using CupCake.Messages.Send;

namespace CupCake.Potions
{
    public sealed class PotionService : CupCakeService
    {
        private readonly Dictionary<Potion, int> _potionCounts = new Dictionary<Potion, int>();

        public ReadOnlyDictionary<Potion, int> PotionCounts
        {
            get { return new ReadOnlyDictionary<Potion, int>(this._potionCounts); }
        }

        private void SetPotion(Potion potion, int value)
        {
            this._potionCounts[potion] = value;

            this.SynchronizePlatform.Do(() =>
                this.Events.Raise(new PotionCountEvent(potion, value)));
        }

        protected override void Enable()
        {
            this.Events.Bind<InitReceiveEvent>(this.OnPotionCount, EventPriority.High);
            this.Events.Bind<PotionCountReceiveEvent>(this.OnPotionCount, EventPriority.High);
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

        public void UsePotion(Potion pot)
        {
            this.Events.Raise(new PotionSendEvent(pot));
        }
    }
}