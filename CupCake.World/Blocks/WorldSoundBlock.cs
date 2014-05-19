using CupCake.EE.Blocks;

namespace CupCake.World.Blocks
{
    public class WorldSoundBlock : WorldBlock
    {
        private readonly int _soundId;

        public WorldSoundBlock(SoundBlock block, int soundId)
            : base((Block)block)
        {
            this._soundId = soundId;
        }

        public override BlockType BlockType
        {
            get { return BlockType.Sound; }
        }

        public int SoundId
        {
            get { return this._soundId; }
        }
    }
}