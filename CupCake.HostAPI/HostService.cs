using CupCake.Core;

namespace CupCake.HostAPI
{
    public class HostService : CupCakeService
    {
        protected override void Enable()
        {
        }

        public void Shutdown()
        {
            this.Events.Raise(new ShutdownRequestEvent(false));
        }

        public void Restart()
        {
            this.Events.Raise(new ShutdownRequestEvent(true));
        }
    }
}