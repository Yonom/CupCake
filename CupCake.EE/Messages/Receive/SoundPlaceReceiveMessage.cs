using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class SoundPlaceReceiveMessage : BlockPlaceReceiveMessage
    {
        //2
        public readonly SoundBlock SoundBlock;
        //3

        public readonly int SoundId;

        internal SoundPlaceReceiveMessage(Message message)
            : base(message, Layer.Foreground, message.GetInteger(0), message.GetInteger(1), (Block)message.GetInteger(2))
        {
            this.SoundBlock = (SoundBlock)message.GetInteger(2);
            this.SoundId = message.GetInteger(3);
        }
    }
}