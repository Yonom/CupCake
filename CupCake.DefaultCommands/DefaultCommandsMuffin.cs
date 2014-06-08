using CupCake.DefaultCommands.Commands;
using CupCake.DefaultCommands.Commands.User;

namespace CupCake.DefaultCommands
{
    public class DefaultCommandsMuffin : CupCakeMuffin<DefaultCommandsMuffin>
    {
        protected override void Enable()
        {
            this.EnablePart<HelpCommand>();
            this.EnablePart<KickCommand>();
            this.EnablePart<KillCommand>();
            this.EnablePart<TeleportCommand>();
        }
    }
}