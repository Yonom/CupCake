using CupCake.DefaultCommands.Commands.Edit;

namespace CupCake.DefaultCommands
{
    public sealed class EditCommandsMuffin : CupCakeMuffin<EditCommandsMuffin>
    {
        public GodModeCommand GodModeCommand { get; private set; }

        protected override void Enable()
        {
            this.GodModeCommand = this.EnablePart<GodModeCommand>();
        }
    }
}