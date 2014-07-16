using CupCake.Command;
using CupCake.HostAPI.IO;

namespace CupCake.Server.Muffins
{
    public class IOMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            Events.Bind<InputEvent>(OnInput);
        }

        private void OnInput(object sender, InputEvent e)
        {
            CommandService.InvokeFromConsole(new ParsedCommand(e.Input));
        }
    }
}