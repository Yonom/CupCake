using System.Runtime.InteropServices;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands.User
{
    public sealed class KickCommand : UserCommandBase
    {
        [MinGroup(Group.Trusted)]
        [Label("kick", "kickplayer")]
        [CorrectUsage("[player] [reason]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            Player player = this.GetPlayerOrSelf(source, message);
            this.RequireSameRank(source, player);

            this.Chatter.ChatService.Kick(source.Name, player.Username, (message.Count > 1 ? message.GetTrail(1) : "Tsk tsk tsk"));
        }
    }
}