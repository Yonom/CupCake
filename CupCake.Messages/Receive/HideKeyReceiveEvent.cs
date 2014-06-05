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
            this.Keys = new Key[Convert.ToInt32(message.Count - 1u) + 1];
            for (uint i = 0; i <= message.Count - 1u; i++)
            {
                this.Keys[Convert.ToInt32(i)] = (Key)Enum.Parse(typeof(Key), message.GetString(i), true);
            }
        }

        public Key[] Keys { get; set; }
    }
}