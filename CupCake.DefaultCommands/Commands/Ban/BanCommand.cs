using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Ban
{
    public sealed class BanCommand : BanCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Operator)]
        [Command("ban", "banplayer")]
        [CorrectUsage("player [reason]")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            if (message.Count >= 2)
            {
                this.Ban(source, message.Args[0], message.GetTrail(1));
            }
            else
            {
                this.Ban(source, message.Args[0]);
            }
        }
    }
}