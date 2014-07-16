using CupCake.Core.Events;

namespace CupCake.Room
{
    public class MetaRoomEvent : Event
    {
        internal MetaRoomEvent(string ownerUsername, int plays, int currentWoots, int totalWoots, string worldName)
        {
            OwnerUsername = ownerUsername;
            Plays = plays;
            CurrentWoots = currentWoots;
            TotalWoots = totalWoots;
            WorldName = worldName;
        }

        public int CurrentWoots { get; private set; }
        public string OwnerUsername { get; private set; }
        public int Plays { get; private set; }
        public int TotalWoots { get; private set; }
        public string WorldName { get; private set; }
    }
}