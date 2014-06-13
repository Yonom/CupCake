using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.Blocks;
using CupCake.Messages.User;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    class UsePotionCommand : UtilityCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Operator)]
        [Label("usepotion", "usepot")]
        [CorrectUsage("potion")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            Potion pot;
            try
            {
                pot = (Potion)Enum.Parse(typeof(Potion), message.Args[0], true);
            }
            catch (Exception ex)
            {
                throw new CommandException("Unable to parse parameter: potion", ex);
            }

            this.PotionService.UsePotion(pot);

            source.Reply("Potion {0} used.", pot);
        }
    }
}
