using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;
using CupCake.Players;

namespace CupCake.DefaultCommands.Commands.User
{
    public sealed class TeleportCommand : UserCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("tele", "teleplayer", "teleport", "teleportplayer")]
        [CorrectUsage("player [target]")]
        [CorrectUsage("player x y")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            RequireOwner();
            Player player = PlayerService.MatchPlayer(message.Args[0]);
            RequireSameRank(source, player);

            if (message.Count >= 3)
            {
                int x = message.GetInt(1);
                int y = message.GetInt(2);

                Chatter.Teleport(player.Username, x, y);
            }
            else if (message.Count == 2)
            {
                try
                {
                    Player target = PlayerService.MatchPlayer(message.Args[1]);
                    Chatter.Teleport(player.Username, target.BlockX, target.BlockY);
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

                    Chatter.Teleport(player.Username, x, y);
                }
                else
                {
                    Chatter.Teleport(player.Username);
                }
            }

            source.Reply("Teleported {0}.", player.ChatName);
        }
    }
}