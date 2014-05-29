using System;
using CupCake.Permissions;

namespace CupCake.Command.Source
{
    public class ExternalInvokeSource : IInvokeSource
    {
        private readonly Action<string> _onReply;

        public ExternalInvokeSource(object sender, Group @group, string name, Action<string> onReply)
        {
            this.Name = name;
            this._onReply = onReply;
            this.Group = @group;
            this.Sender = sender;
        }

        public string Name { get; set; }
        public object Sender { get; private set; }
        public Group Group { get; private set; }

        public void Reply(string message)
        {
            this._onReply(message);
        }
    }
}