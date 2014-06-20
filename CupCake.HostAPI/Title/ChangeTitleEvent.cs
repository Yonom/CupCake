using CupCake.Core.Events;

namespace CupCake.HostAPI.Title
{
    public class ChangeTitleEvent : Event
    {
        internal ChangeTitleEvent(string newTitle)
        {
            this.NewTitle = newTitle;
        }

        public string NewTitle { get; set; }
    }
}