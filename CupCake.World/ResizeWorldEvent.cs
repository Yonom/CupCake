using CupCake.Core.Events;

namespace CupCake.World
{
    public class ResizeWorldEvent : Event
    {
        internal ResizeWorldEvent(int newWidth, int newHeight)
        {
            this.NewHeight = newHeight;
            this.NewWidth = newWidth;
        }

        public int NewHeight { get; private set; }
        public int NewWidth { get; private set; }
    }
}