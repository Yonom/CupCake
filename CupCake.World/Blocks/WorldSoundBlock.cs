using CupCake.EE.Blocks;
using CupCake.EE.Events.Send;

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

        protected override bool Equals(BlockPlaceSendEvent other)
        {
            var soundEvent = other as SoundPlaceSendEvent;
            if (soundEvent != null)
                return base.Equals(soundEvent) && soundEvent.SoundId == this.SoundId;

            return base.Equals(other);
        }

        protected override bool Equals(WorldBlock other)
        {
            var soundBlock = other as WorldSoundBlock;
            if (soundBlock != null)
                return base.Equals(soundBlock) && soundBlock.SoundId == this.SoundId;

            return base.Equals(other);
        }
    }
}