using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Send
{
    public class SoundPlaceSendEvent : SendEvent, IBlockPlaceSendEvent
    {
        public SoundPlaceSendEvent(Layer layer, int x, int y, SoundBlock block, int soundId)
        {
            Block = block;
            X = x;
            Y = y;
            Layer = BlockUtils.CorrectLayer((Block) block, layer);

            SoundId = soundId;
        }

        public SoundBlock Block { get; set; }
        public int SoundId { get; set; }

        Block IBlockPlaceSendEvent.Block
        {
            get { return (Block) Block; }
            set { Block = (SoundBlock) value; }
        }

        public Layer Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Encryption { get; set; }

        public override Message GetMessage()
        {
            return Message.Create(Encryption, (int) Layer, X, Y, (int) Block, SoundId);
        }
    }
}