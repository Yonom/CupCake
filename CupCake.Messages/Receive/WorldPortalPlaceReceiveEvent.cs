using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class WorldPortalPlaceReceiveEvent : BlockPlaceReceiveEvent
    {
        public WorldPortalPlaceReceiveEvent(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.WorldPortalBlock = (WorldPortalBlock)message.GetInteger(2);
            this.WorldPortalTarget = message.GetString(3);
        }

        public WorldPortalBlock WorldPortalBlock { get; set; }
        public string WorldPortalTarget { get; set; }
    }
}