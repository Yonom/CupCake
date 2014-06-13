using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Core.Events;
using CupCake.Messages.Blocks;

namespace CupCake.Potions
{
    public class PotionCountEvent : Event
    {
        public Potion Potion { get; private set; }
        public int Count { get; private set; }

        public PotionCountEvent(Potion potion, int count)
        {
            this.Potion = potion;
            this.Count = count;
        }
    }
}
