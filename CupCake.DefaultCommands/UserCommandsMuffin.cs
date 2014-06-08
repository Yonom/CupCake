using CupCake.DefaultCommands.Commands;
using CupCake.DefaultCommands.Commands.User;

namespace CupCake.DefaultCommands
{
    public class UserCommandsMuffin : CupCakeMuffin<UserCommandsMuffin>
    {
        protected override void Enable()
        {
            this.EnablePart<KickCommand>();
            this.EnablePart<KillCommand>();
            this.EnablePart<TeleportCommand>();
        }
    }
}