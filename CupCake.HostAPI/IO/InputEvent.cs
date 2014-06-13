using CupCake.Core.Events;

namespace CupCake.HostAPI.IO
{
    public class InputEvent : Event
    {
        public InputEvent(string input)
        {
            this.Input = input;
        }

        public string Input { get; set; }
    }
}