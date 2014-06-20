using CupCake.Core.Events;
using CupCake.Messages.User;

namespace CupCake.Potions
{
    public class PotionCountEvent : Event
    {
        internal PotionCountEvent(Potion potion, int count)
        {
            this.Potion = potion;
            this.Count = count;
        }

        public Potion Potion { get; private set; }
        public int Count { get; private set; }
    }
}