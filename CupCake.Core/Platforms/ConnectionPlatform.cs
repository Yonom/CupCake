using MuffinFramework.Platforms;
using PlayerIOClient;

namespace CupCake.Core.Platforms
{
    public class ConnectionPlatform : Platform
    {
        public Connection Connection { get; set; }

        protected override void Enable()
        {
        }
    }
}