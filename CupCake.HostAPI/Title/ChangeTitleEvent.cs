using CupCake.Core.Events;

namespace CupCake.HostAPI.Title
{
    public class ChangeTitleEvent : Event
    {
        public ChangeTitleEvent(string newTitle)
        {
            this.NewTitle = newTitle;
        }

        public string NewTitle { get; set; }
    }
}