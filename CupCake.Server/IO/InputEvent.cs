using CupCake.Core.Events;

namespace CupCake.Server.IO
{
    public class InputEvent : Event
    {
        public string Input { get; set; }

        public InputEvent(string input)
        {
            this.Input = input;
        }
    }
}
