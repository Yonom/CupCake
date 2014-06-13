using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Core.Events;
using CupCake.Messages.Receive;

namespace CupCake.Room
{
    public class MetaRoomEvent : Event
    {
        public MetaRoomEvent(string ownerUsername, int plays, int currentWoots, int totalWoots, string worldName)
        {
            this.OwnerUsername = ownerUsername;
            this.Plays = plays;
            this.CurrentWoots = currentWoots;
            this.TotalWoots = totalWoots;
            this.WorldName = worldName;
        }

        public int CurrentWoots { get; private set; }
        public string OwnerUsername { get; private set; }
        public int Plays { get; private set; }
        public int TotalWoots { get; private set; }
        public string WorldName { get; private set; }
    }
}
