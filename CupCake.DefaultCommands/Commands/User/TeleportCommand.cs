using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands.User
{
    public class TeleportCommand : UserCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("tele", "teleport", "teleportplayer")]
        [CorrectUsage("player [target]")]
        [CorrectUsage("player x y")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RequireOwner();
            Player player = this.PlayerService.MatchPlayer(message.Args[0]);
            this.RequireSameRank(source, player);

            if (message.Count >= 3)
            {
                int x = message.GetInt(1);
                int y = message.GetInt(2);

                this.Chatter.Teleport(player.Username, x, y);
            }
            else if (message.Count == 2)
            {
                try
                {
                    Player target = this.PlayerService.MatchPlayer(message.Args[1]);
                    this.Chatter.Teleport(player.Username, target.BlockX, target.BlockY);
                }
                catch (CommandException ex)
                {
                    throw new CommandException(ex.Message + " Parameter: target", ex);
                }
            }
            else
            {
                var playerSource = source as PlayerInvokeSource;
                if (playerSource != null)
                {
                    int x = playerSource.Player.BlockX;
                    int y = playerSource.Player.BlockX;

                    this.Chatter.Teleport(player.Username, x, y);
                }
                else
                {
                    this.Chatter.Teleport(player.Username);
                }
            }

            source.Reply("Teleported {0}.", player.ChatName);
        }
    }
}