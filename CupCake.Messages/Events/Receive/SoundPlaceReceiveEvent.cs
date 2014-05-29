using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class SoundPlaceReceiveEvent : BlockPlaceReceiveEvent
    {
        public SoundPlaceReceiveEvent(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2)
                )
        {
            this.SoundBlock = (SoundBlock)message.GetInteger(2);
            this.SoundId = message.GetInteger(3);
        }

        public SoundBlock SoundBlock { get; set; }
        public int SoundId { get; set; }
    }
}