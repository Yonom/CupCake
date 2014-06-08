using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.User
{
    public class TeleportCommand : CommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Label("tele", "teleport", "teleportplayer")]
        [CorrectUsage("player [target]")]
        [CorrectUsage("player x y")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            //this.RequireOwner();
            var player = this.PlayerService.MatchPlayer(message.Args[0]);
            this.RequirePermissions(source, player);

            if (message.Count >= 3)
            {
                var x = message.GetInt(1);
                var y = message.GetInt(2);

                this.Chatter.Teleport(player.Username, x, y);
            }
            else if (message.Count == 2)
            {
                try
                {
                    var target = this.PlayerService.MatchPlayer(message.Args[1]);
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
                    var x = playerSource.Player.BlockX;
                    var y = playerSource.Player.BlockX;

                    this.Chatter.Teleport(player.Username, x, y);
                }
                else
                {
                    this.Chatter.Teleport(player.Username);
                }
            }

            source.Reply("Teleported.");
        }
    }
}
