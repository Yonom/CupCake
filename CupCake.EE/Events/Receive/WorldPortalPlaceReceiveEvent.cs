using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class WorldPortalPlaceReceiveEvent : BlockPlaceReceiveEvent
    {
        public WorldPortalPlaceReceiveEvent(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.WorldPortalBlock = (WorldPortalBlock)message.GetInteger(2);
            this.WorldPortalTarget = message.GetString(3);
        }

        public WorldPortalBlock WorldPortalBlock { get; private set; }
        public string WorldPortalTarget { get; private set; }
    }
}