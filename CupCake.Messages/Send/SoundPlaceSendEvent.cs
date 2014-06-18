using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class SoundPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        public SoundPlaceSendEvent(Layer layer, int x, int y, SoundBlock block, int soundId)
        {
            this.Block = block;
            this.X = x;
            this.Y = y;
            this.Layer = BlockUtils.CorrectLayer((Block)block, layer);

            this.SoundId = soundId;
        }

        public SoundBlock Block { get; set; }
        public int SoundId { get; set; }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block)this.Block; }
            set { this.Block = (SoundBlock)value; }
        }

        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(this.Encryption, (int)this.Layer, this.X, this.Y, (int)this.Block, this.SoundId);
        }
    }
}