using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Send
{
    public sealed class SoundPlaceSendMessage : BlockPlaceSendMessage
    {
        public int SoundId { get; set; }

        public SoundPlaceSendMessage(string encryption, Layer layer, int x, int y, SoundBlock block, int soundId)
            : base(encryption, layer, x, y, (Block)block)
        {
            this.SoundId = soundId;
        }

        public override Message GetMessage()
        {
            if (IsSound(this.Block))
            {
                Message message = base.GetMessage();
                message.Add(this.SoundId);
                return message;
            }
            return base.GetMessage();
        }
    }
}