using System.Diagnostics;
using CupCake.Permissions;

namespace CupCake.Command.Source
{
    [DebuggerDisplay("Name = {Name}")]
    public class ExternalInvokeSource : InvokeSourceBase
    {
        public ExternalInvokeSource(object sender, Group @group, string name, ReplyCallback onReply)
            : base(sender, group, onReply)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}