using CupCake.Command;
using CupCake.Core;
using CupCake.HostAPI.IO;

namespace CupCake.Server.Muffins
{
    public class IOMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
        }

        [EventListener]
        private void OnInput(InputEvent e)
        {
            this.CommandService.InvokeFromConsole(new ParsedCommand(e.Input));
        }
    }
}