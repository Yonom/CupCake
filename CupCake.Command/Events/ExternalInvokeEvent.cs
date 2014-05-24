using CupCake.Command.Source;
using CupCake.Core.Events;
using CupCake.Permissions;

namespace CupCake.Command.Events
{
    public class ExternalInvokeEvent : Event
    {
        public ExternalInvokeEvent(ExternalInvokeSource source, string message, Group group = Group.Host)
        {
            this.Source = source;
            this.Message = message;
            this.Group = @group;
        }

        public ExternalInvokeSource Source { get; private set; }
        public string Message { get; set; }
        public Group Group { get; set; }
    }
}