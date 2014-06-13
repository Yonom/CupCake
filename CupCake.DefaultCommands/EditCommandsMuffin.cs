using CupCake.DefaultCommands.Commands.Edit;

namespace CupCake.DefaultCommands.Commands
{
    public class EditCommandsMuffin : CupCakeMuffin<EditCommandsMuffin>
    {
        protected override void Enable()
        {
            this.EnablePart<GodModeCommand>();
        }
    }
}