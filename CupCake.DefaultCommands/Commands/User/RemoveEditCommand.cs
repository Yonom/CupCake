using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands.User
{
    public sealed class RemoveEditCommand : UserCommandBase
    {
        [MinGroup(Group.Moderator)]
        [Label("removeedit", "removeeditplayer")]
        [CorrectUsage("[player]")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            RequireOwner();
            Player player = GetPlayerOrSelf(source, message);
            RequireSameRank(source, player);

            Chatter.RemoveEdit(player.Username);

            source.Reply("Removed edit from {0}.", player.ChatName);
        }
    }
}