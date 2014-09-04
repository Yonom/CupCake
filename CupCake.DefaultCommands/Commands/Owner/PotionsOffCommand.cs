using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Owner
{
    public sealed class PotionsOffCommand : OwnerCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Command("potionsoff")]
        [CorrectUsage("potion1 [potion2 [...]]")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            this.Chatter.PotionsOff(message.Args);
            source.Reply("Disabled potions: {0}", message.GetTrail(0));
        }
    }
}