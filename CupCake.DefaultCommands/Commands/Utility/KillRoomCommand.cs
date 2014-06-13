using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class KillRoomCommand : UtilityCommandBase
    {
        [MinGroup(Group.Operator)]
        [Label("killroom")]
        [CorrectUsage("")]
        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.RoomService.KillRoom();
        }
    }
}