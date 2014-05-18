using CupCake.Utils.Blocks;
using PlayerIOClient;

namespace CupCake.Utils.Messages.Send
{
    public sealed class WorldPortalPlaceSendMessage : BlockPlaceSendMessage
    {
        public readonly string WorldPortalTarget;

        public WorldPortalPlaceSendMessage(string encryption, Layer layer, int x, int y, WorldPortalBlock block,
            string worldPortalTarget)
            : base(encryption, layer, x, y, (Block)block)
        {
            this.WorldPortalTarget = worldPortalTarget;
        }

        internal override Message GetMessage()
        {
            if (IsWorldPortal(this.Block))
            {
                Message message = base.GetMessage();
                message.Add(this.WorldPortalTarget);
                return message;
            }
            return base.GetMessage();
        }
    }
}