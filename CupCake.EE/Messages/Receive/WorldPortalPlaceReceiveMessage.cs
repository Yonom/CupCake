using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class WorldPortalPlaceReceiveMessage : BlockPlaceReceiveMessage
    {
        public WorldPortalBlock WorldPortalBlock { get; private set; }
        public string WorldPortalTarget { get; private set; }

        public WorldPortalPlaceReceiveMessage(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.WorldPortalBlock = (WorldPortalBlock)message.GetInteger(2);
            this.WorldPortalTarget = message.GetString(3);
        }
    }
}