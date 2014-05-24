using System;
using CupCake.Messages.Blocks;
using PlayerIOClient;

namespace CupCake.Messages.Events.Receive
{
    public class ShowKeyReceiveEvent : ReceiveEvent
    {
        public ShowKeyReceiveEvent(Message message)
            : base(message)
        {
            this.Keys = new Key[Convert.ToInt32(message.Count - 1) + 1];
            for (uint i = 0; i <= message.Count - 1u; i++)
            {
                this.Keys[Convert.ToInt32(i)] = (Key)Enum.Parse(typeof(Key), message.GetString(i), true);
            }
        }

        public Key[] Keys { get; private set; }
    }
}