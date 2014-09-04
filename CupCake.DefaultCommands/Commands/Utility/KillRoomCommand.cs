using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class KillRoomCommand : UtilityCommandBase
    {
        [MinGroup(Group.Operator)]
        [Command("killroom")]
        [CorrectUsage("")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RoomService.KillRoom();
        }
    }
}