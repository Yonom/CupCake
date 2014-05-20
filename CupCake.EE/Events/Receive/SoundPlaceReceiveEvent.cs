using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Events.Receive
{
    public sealed class SoundPlaceReceiveEvent : BlockPlaceReceiveEvent
    {
        public SoundPlaceReceiveEvent(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.SoundBlock = (SoundBlock)message.GetInteger(2);
            this.SoundId = message.GetInteger(3);
        }

        public SoundBlock SoundBlock { get; private set; }
        public int SoundId { get; private set; }
    }
}