using System;

namespace CupCake.Messages.Blocks
{
    public enum SoundBlock
    {
        [Obsolete("Use SoundBlock.MusicPiano instead.")]
        BlockMusicPiano = Block.MusicPiano,
        [Obsolete("Use SoundBlock.MusicDrum instead.")]
        BlockMusicDrum = Block.MusicDrum,

        MusicPiano = Block.MusicPiano,
        MusicDrum = Block.MusicDrum
    }
}