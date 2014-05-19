using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class WorldPortalPlaceSendMessage : BlockPlaceSendMessage
    {
        public string WorldPortalTarget { get; set; }

        public WorldPortalPlaceSendMessage(string encryption, Layer layer, int x, int y, WorldPortalBlock block,
            string worldPortalTarget)
            : base(encryption, layer, x, y, (Block)block)
        {
            this.WorldPortalTarget = worldPortalTarget;
        }

        public override Message GetMessage()
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