using CupCake.Core.Events;

namespace CupCake.HostAPI
{
    public class ShutdownRequestEvent : Event
    {
        internal ShutdownRequestEvent(bool isRestarting)
        {
            this.IsRestarting = isRestarting;
        }

        public bool IsRestarting { get; private set; }
    }
}