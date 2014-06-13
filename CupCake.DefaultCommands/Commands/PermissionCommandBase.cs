using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands
{
    public abstract class PermissionCommandBase : CommandBase<PermissionMuffin>
    {
        protected void RunPermissionCommand(IInvokeSource source, ParsedCommand message, Group permissions)
        {
            string name = message.Args[0];
            this.PlayerService.MatchPlayer(name, player =>
            {
                this.RequireHigherRank(source, player);

                player.SetGroup(permissions);

                source.Reply("{0} is now {1}.", player.ChatName, permissions);
            }, username =>
            {
                this.Host.SetPermission(username, permissions);
                source.Reply("{0} is now {1}.", PlayerUtils.GetChatName(username), permissions);
            });
        }
    }
}