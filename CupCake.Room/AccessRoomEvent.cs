using CupCake.Core.Events;
using CupCake.Messages.User;

namespace CupCake.Room
{
    public class AccessRoomEvent : Event
    {
        public AccessRoomEvent(AccessRight newRights)
        {
            this.NewRights = newRights;
        }

        public AccessRight NewRights { get; private set; }
    }
}