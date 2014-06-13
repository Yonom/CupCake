using System;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Messages.User;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class UsePotionCommand : UtilityCommandBase
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

            try
            {
                this.PotionService.UsePotion(pot);
            }
            catch (InvalidOperationException ex)
            {
                throw new CommandException("Unable to use potion: " + ex.Message, ex);
            }

            source.Reply("Potion {0} used.", pot);
        }
    }
}