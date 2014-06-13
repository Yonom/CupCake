using CupCake.Core.Events;

namespace CupCake.HostAPI.Status
{
    public class ChangeStatusEvent : Event
    {
        public ChangeStatusEvent(string newStatus)
        {
            this.NewStatus = newStatus;
        }

        public string NewStatus { get; set; }
    }
}