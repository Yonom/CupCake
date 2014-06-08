using CupCake.Command;
using CupCake.HostAPI.IO;

namespace CupCake.Server.Muffins
{
    public class IOMuffin : CupCakeMuffin
    {
        protected override void Enable()
        {
            this.Events.Bind<InputEvent>(this.OnInput);
        }

        private void OnInput(object sender, InputEvent e)
        {
            this.CommandService.InvokeFromConsole(new ParsedCommand(e.Input));
        }
    }
}