using CupCake.DefaultCommands.Commands.Edit;

namespace CupCake.DefaultCommands.Commands
{
    public sealed class EditCommandsMuffin : CupCakeMuffin<EditCommandsMuffin>
    {
        protected override void Enable()
        {
            this.GodModeCommand = this.EnablePart<GodModeCommand>();
        }

        public GodModeCommand GodModeCommand { get; private set; }
    }
}