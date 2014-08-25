using MuffinFramework.Platforms;
using PlayerIOClient;

namespace CupCake.Core
{
    public class ConnectionPlatform : Platform
    {
        public Connection Connection { get; set; }

        public string WorldId { get; set; }

        protected override void Enable()
        {
        }
    }
}