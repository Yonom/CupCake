using CupCake.Permissions;

namespace CupCake.Command.Source
{
    public class ConsoleInvokeSource : InvokeSourceBase
    {
        public ConsoleInvokeSource(object sender, Group @group, ReplyCallback onReply)
            : base(sender, @group, "Console", onReply)
        {
        }
    }
}
