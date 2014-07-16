using CupCake.Core.Events;

namespace CupCake.HostAPI.IO
{
    public class InputEvent : Event
    {
        public InputEvent(string input)
        {
            Input = input;
        }

        public string Input { get; set; }
    }
}