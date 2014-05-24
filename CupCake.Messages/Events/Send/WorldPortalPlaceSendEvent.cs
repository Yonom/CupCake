using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Events.Send
{
    public class WorldPortalPlaceSendEvent : BlockPlaceSendEvent
    {
        public WorldPortalPlaceSendEvent(Layer layer, int x, int y, WorldPortalBlock block,
            string worldPortalTarget)
            : base(layer, x, y, (Block)block)
        {
            this.WorldPortalTarget = worldPortalTarget;
        }

        public string WorldPortalTarget { get; set; }

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