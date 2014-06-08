using CupCake.Core.Events;

namespace CupCake.HostAPI.Title
{
    public class ChangeTitleEvent : Event
    {
        public string NewTitle { get; set; }

        public ChangeTitleEvent(string newTitle)
        {
            this.NewTitle = newTitle;
        }
    }
}
