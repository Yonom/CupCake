using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Permissions;

namespace CupCake.DefaultCommands.Commands.Utility
{
    public class SayCommand : UtilityCommandBase
    {
        [MinArgs(1)]
        [MinGroup(Group.Moderator)]
        [Command("say")]
        [CorrectUsage("text")]
        private void Run(IInvokeSource source, ParsedCommand message)
        {
            this.Chatter.ChatService.Chat(message.GetTrail(0), source.Name);
        }
    }
}