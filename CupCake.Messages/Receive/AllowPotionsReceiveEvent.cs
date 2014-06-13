using System;
using System.Collections.Generic;
using CupCake.Messages.User;
using PlayerIOClient;

namespace CupCake.Messages.Receive
{
    public class AllowPotionsReceiveEvent : ReceiveEvent
    {
        public AllowPotionsReceiveEvent(Message message)
            : base(message)
        {
            this.Allowed = message.GetBoolean(0);

            var potsList = new List<Potion>();
            for (uint i = 1; i <= message.Count - 1; i += 1)
            {
                potsList.Add((Potion)Int32.Parse(message.GetString(i)));
            }
            this.DisabledPotions = potsList.ToArray();
        }

        public bool Allowed { get; set; }
        public Potion[] DisabledPotions { get; set; }
    }
}