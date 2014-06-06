using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class HideKeyReceiveEvent : ReceiveEvent
    {
        public HideKeyReceiveEvent(Message message)
            : base(message)
        {
            this.Keys = new Key[message.Count];
            for (uint i = 0; i <= message.Count - 1u; i++)
            {
                this.Keys[(int)i] = (Key)Enum.Parse(typeof(Key), message.GetString(i), true);
            }
        }

        public Key[] Keys { get; set; }
    }
}