using CupCake.DefaultCommands.Commands;

namespace CupCake.DefaultCommands
{
    public class DefaultCommandsMuffin : CupCakeMuffin<DefaultCommandsMuffin>
    {
        protected override void Enable()
        {
            this.EnablePart<KickCommand>();
        }
    }
}