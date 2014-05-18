using System;
using CupCake.EE.Blocks;
using PlayerIOClient;

namespace CupCake.EE.Messages.Receive
{
    public sealed class HideKeyReceiveMessage : ReceiveMessage
    {
        //0

        public readonly Key[] Keys;

        internal HideKeyReceiveMessage(Message message)
            : base(message)
        {
            this.Keys = new Key[Convert.ToInt32(message.Count - 1u) + 1];
            for (uint i = 0; i <= message.Count - 1u; i++)
            {
                this.Keys[Convert.ToInt32(i)] = (Key)Enum.Parse(typeof(Key), message.GetString(i), true);
            }
        }
    }
}