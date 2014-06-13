using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public sealed class PotionsOffCommand : OwnerCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("potionsoff")]
        [CorrectUsage("potion1 [potion2 [...]]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            this.Chatter.PotionsOff(message.Args);
            source.Reply("Disabled potions.");
        }
    }
}